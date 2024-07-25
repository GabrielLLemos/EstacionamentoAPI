using EstacionamentoAPI.Entities;
namespace EstacionamentoAPI.Repositories
{
    public interface IEstacionamentoRepository
    {
        Task<IEnumerable<Ticket>> ListData();
        Task<Ticket> CheckInTicket(decimal? price);
        Task<Ticket> UpdateData(Guid id, DateTime? updateCheckOut, DateTime? updateCheckIn, decimal? price);
        Task<Ticket> GetById(Guid id);
        //Task<string> FindCode(string code);
        Task Delete(Guid id);
        Task<bool> FindStatusById(Guid id);
    }
}
