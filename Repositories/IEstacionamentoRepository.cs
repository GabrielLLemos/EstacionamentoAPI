using EstacionamentoAPI.Proprieties;
using Microsoft.AspNetCore.Http.Timeouts;

namespace EstacionamentoAPI.Repositories
{
    public interface IEstacionamentoRepository
    {
        IEnumerable<Carro> GetCarros();
        Carro GetById(Guid id);
        Carro EntradaCarro(string tipo);
        Carro SaidaCarro(Guid id);
        Carro AlterarDados(Guid id, string tipo);

    }
}
