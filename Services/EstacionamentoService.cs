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

        public Ticket CheckIn(decimal? price)
        {
            var newCheckIn = _estacionamentoRepository.CheckInTicket(price);
            var code = TicketCodeHelper.GenerateTicketCode(TicketConstant.CodeSize);
            var existingCode = _estacionamentoRepository.FindCode(code);


            while (existingCode != null)
            {
                code = TicketCodeHelper.GenerateTicketCode(TicketConstant.CodeSize);
            }

            newCheckIn.SetCode(code);
            return newCheckIn;
        }

        //CheckOut
        public Ticket CheckOut(Guid id)
        {
            var existingTicket = _estacionamentoRepository.CheckOutTicket(id);
            
            return existingTicket;
        }

        //Update
        public Ticket Update(Guid id, DateTime? updateCheckIn, decimal? price)
        {
            var update = _estacionamentoRepository.UpdateData(id, updateCheckIn, price);
            var parkingStatus = _estacionamentoRepository.CheckStatus(id);

            if (parkingStatus != false)
                throw new ArgumentException("Não é possível fazer alterações, pois o veículo já saiu do estacionamento.");

            return update;
        }
    }
}