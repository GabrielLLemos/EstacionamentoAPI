using EstacionamentoAPI.Entities;

namespace EstacionamentoAPI.Services
{
    public interface IEstacionamentoService
    {
        Ticket CheckIn(decimal? price);
        Ticket CheckOut(Guid id);
        Ticket Update(Guid id, DateTime? updateCheckIn, decimal? price);
    }
}
