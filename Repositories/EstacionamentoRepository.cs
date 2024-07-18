using EstacionamentoAPI.Entities;
using EstacionamentoAPI.Settings;
using MongoDB.Driver;
using SharpCompress.Common;
using System.Diagnostics.CodeAnalysis;

namespace EstacionamentoAPI.Repositories
{
    public class EstacionamentoRepository : IEstacionamentoRepository
    {
        //Criando uma lista que irá armazenar os carros. Será utilizado até a criação do Banco de dados.
        private static List<Ticket> _tickets = new List<Ticket>();
        private readonly IMongoCollection<Ticket> _ticketCollection;

        public EstacionamentoRepository(IMongoClient mongoClient, IMongoDbSettings settings)
        {
            var database = mongoClient.GetDatabase(settings.DatabaseName);
            _ticketCollection = database.GetCollection<Ticket>("Tickets");
        }


        //Criar Ticket
        public async Task<Ticket> CheckInTicket(decimal? price)
        {
            var ticket = new Ticket(price);

            await _ticketCollection.InsertOneAsync(ticket);

            return ticket;
        }

        //Deletar Ticket
        public async Task Delete(Guid id)
        {
            await _ticketCollection.DeleteOneAsync(ticket => ticket.Id == id);
        }

        //Buscar um Ticket específico
        public async Task<Ticket> GetById(Guid id)
        {
            return await _ticketCollection.Find(ticket => ticket.Id == id).FirstOrDefaultAsync();
        }

        //Encontrar código
        public async Task<string> FindCode(string code)
        {
            var ticket = await _ticketCollection.Find(ticket => ticket.Code == code).FirstOrDefaultAsync();

            return ticket.Code;
        }

        //Listar Tickets
        public async Task<IEnumerable<Ticket>> ListData()
        {
            return await _ticketCollection.Find(Ticket => true).ToListAsync();
        }

        //Alterar Tickets
        public async Task<Ticket> UpdateData(Guid id, DateTime? updateCheckIn, decimal? price)
        {
            // "construtor pra atualizar --> contém as configurações pra fazer o update"
            var update = Builders<Ticket>.Update;

            // adicionando as propriedades e os valores que serão atualizados
            var updateDefinition = update
                .Set(nameof(updateCheckIn), updateCheckIn)
              .Set(nameof(price), price);

            // faz a atualização
            var result = await _ticketCollection.FindOneAndUpdateAsync(
               Builders<Ticket>.Filter.Where(expression => expression.Id == id), // Filtra pelo Id correspondente
               updateDefinition,// Aplica as definições de atualização
                                // Opções para a operação FindOneAndUpdate:
                new FindOneAndUpdateOptions<Ticket, Ticket>
                {
                    ReturnDocument = ReturnDocument.After //Retorna o documento atualizado };
                });

            return result;
        }

        //CheckOut
        public async Task<Ticket> CheckOutTicket(Guid id)
        {
            var ticket = await _ticketCollection.Find(ticket => ticket.Id == id).FirstOrDefaultAsync();
        } 

        //Check Status
        public bool CheckStatus(Guid id)
        {
            var existingTicket = _tickets.Find(t => t.Id == id);

            if(existingTicket == null)
                throw new ArgumentException("Não foi possível encontrar o Ticket");
            
            return existingTicket.Status;
        }
    }
}
