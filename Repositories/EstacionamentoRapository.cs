using EstacionamentoAPI.Proprieties;

namespace EstacionamentoAPI.Repositories
{
    public class EstacionamentoRapository : IEstacionamentoRepository
    {
        //Criando uma lista que irá armazenar os carros. Será utilizado até a criação do Banco de dados.
        private static List<Carro> _carros = new List<Carro>();


        public Carro EntradaCarro(string tipoDeCarro)
        {
            var carro = new Carro(tipoDeCarro);

            carro.SetTipo(tipoDeCarro);
            _carros.Add(carro);

            return carro;
        }

        public Carro GetById(Guid id)
        {
            return _carros.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Carro> GetCarros()
        {
            return _carros;
        }

        public Carro AlterarDados(Guid id, string tipo)
        {
            var carro = _carros.FirstOrDefault(x => x.Id == id);
            carro.SetTipo(tipo);

            return carro;
        }

        public Carro SaidaCarro(Guid id)
        {
            var carro = _carros.FirstOrDefault(x => x.Id == id);

            carro.SetTempo();
            
            return carro;
        }
    }
}
