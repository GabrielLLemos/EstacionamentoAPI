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

        //Listar Tickets
        public async Task<IEnumerable<Ticket>> ListData()
        {
            return await _ticketCollection.Find(Ticket => true).ToListAsync();
        }

        //Alterar Tickets
        public async Task<Ticket> UpdateData(Guid id, DateTime? updateCheckOut = null, DateTime? updateCheckIn = null, decimal? price = null)
        {
            // "construtor pra atualizar --> contém as configurações pra fazer o update"
            var update = Builders<Ticket>.Update;

            var updateDefinitions = new List<UpdateDefinition<Ticket>>();

            if (updateCheckIn.HasValue)
            {
                updateDefinitions.Add(update.Set(t => t.CheckInCurrent, updateCheckIn.Value));
            }

            if (updateCheckOut.HasValue)
            {
                updateDefinitions.Add(update.Set(t => t.CheckOutCurrent, updateCheckOut.Value));
            }

            if (price.HasValue)
            {
                updateDefinitions.Add(update.Set(t => t.PricePerHour, price.Value));
            }

            var updateDefinition = update.Combine(updateDefinitions);

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

        //Check Status
        public async Task<bool> FindStatusById(Guid id)
        {
            var ticket = await _ticketCollection.Find(ticket => ticket.Id == id).FirstOrDefaultAsync();

            if (ticket == null)
                throw new ArgumentException("Não foi possível encontrar o Ticket");
            
            return ticket.Status;
        }
    }
}
