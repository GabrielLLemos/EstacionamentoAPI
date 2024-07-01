namespace EstacionamentoAPI.Proprieties
{
    public class Carro
    {
        public Guid Id { get; private set; }
        public string TipoDeVeiculo { get; private set; }
        public DateTime Entrada { get; }
        public DateTime Saida { get; private set; }

        public Carro(string tipoDeVeiculo)
        {
            Id = Guid.NewGuid();
            TipoDeVeiculo = tipoDeVeiculo;
            Entrada = DateTime.Now;
        }

        public void SetTipo(string tipo) => TipoDeVeiculo = tipo;
        public void SetTempo() => Saida = DateTime.Now;
    }
}
