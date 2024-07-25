using EstacionamentoAPI.Helpers;
using EstacionamentoAPI.Constants;
using EstacionamentoAPI.Entities;
using EstacionamentoAPI.Repositories;


namespace EstacionamentoAPI.Services
{
    public class EstacionamentoService : IEstacionamentoService
    {
        private readonly IEstacionamentoRepository _estacionamentoRepository;

        public EstacionamentoService(IEstacionamentoRepository estacionamentoRepository)
        {
            _estacionamentoRepository = estacionamentoRepository;
        }

        public async Task<Ticket> CheckIn(decimal? price)
        {
            var newCheckIn = await _estacionamentoRepository.CheckInTicket(price);

            return newCheckIn;
        }

        //CheckOut
        public async Task<Ticket> CheckOut(Guid id)
        {
            var existingTicket = await _estacionamentoRepository.GetById(id);
            var today = DateTime.Now;

            if (existingTicket.CheckInCurrent > today)
                throw new ArgumentException("Data inválida.");

            await _estacionamentoRepository.UpdateData(id, today, null, null);

            return existingTicket;
        }

        //Update

    }
}