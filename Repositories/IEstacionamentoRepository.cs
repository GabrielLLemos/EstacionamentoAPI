using EstacionamentoAPI.Entities;
namespace EstacionamentoAPI.Repositories
{
    public interface IEstacionamentoRepository
    {
        Task<IEnumerable<Ticket>> ListData();
        Task<Ticket> CheckInTicket(decimal? price);
        Task<Ticket> UpdateData(Guid id, DateTime? updateCheckIn, decimal? price);
        Task<Ticket> GetById(Guid id);
        Task<string> FindCode(string code);
        Task Delete(Guid id);
        Task<bool> CheckStatus(Guid id);
        Task<Ticket> CheckOutTicket(Guid id);
    }
}
