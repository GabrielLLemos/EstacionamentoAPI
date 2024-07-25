using EstacionamentoAPI.Constants;
namespace EstacionamentoAPI.Entities
{
    public class Ticket
    {
        public Guid Id { get; private set; }
        public DateTime? CheckInCurrent { get; private set; }
        public DateTime? CheckOutCurrent { get; private set; }
        public bool Status {  get; private set; }
        public decimal? PricePerHour {  get; private set; }

        public Ticket(decimal? price) 
        {
            Id = Guid.NewGuid();
            CheckInCurrent = DateTime.Now;
            PricePerHour = price ?? TicketConstant.Price;
        }

        public void Exit () => CheckOutCurrent = DateTime.Now;
        public void UpdateCheckIn(DateTime? date) => CheckInCurrent = date;
        public void UpdatePricePerHour(decimal? pricePerHour) => PricePerHour = pricePerHour;
        public void CheckStatus(Guid id)
        {
            if (CheckOutCurrent == null)
            {
                Status = false;
            }
            else
            {
                Status = true;
            }
        }
    }
}
