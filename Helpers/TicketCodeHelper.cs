using System.Text;

namespace EstacionamentoAPI.Helpers
{
    public static class TicketCodeHelper
    {
        private static readonly Random random = new Random();

        public static string GenerateTicketCode(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder ticketCode = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                ticketCode.Append(chars[random.Next(chars.Length)]);
            }
            return ticketCode.ToString();
        }
    }
}
