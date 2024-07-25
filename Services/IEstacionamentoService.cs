using EstacionamentoAPI.Entities;

namespace EstacionamentoAPI.Services
{
    public interface IEstacionamentoService
    {
        Task<Ticket> CheckIn(decimal? price);
        Task<Ticket> CheckOut(Guid id);
    }
}
