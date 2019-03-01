using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TrainTrain.Domain;

namespace TrainTrain.Infra
{
    public class SeatsReservationAdapter
    {
        private readonly ITicketOffice _ticketOffice;

        public SeatsReservationAdapter(ITicketOffice ticketOffice)
        {
            _ticketOffice = ticketOffice;
        }

        public static string AdaptReservation(Reservation reservation)
        {
            return
                $"{{\"train_id\": \"{reservation.TrainId.Id}\", \"booking_reference\": \"{reservation.BookingReference.Id}\", \"seats\": {DumpSeats(reservation.Seats)}}}";
        }

        private static string DumpSeats(IEnumerable<Seat> seats)
        {
            var sb = new StringBuilder("[");

            var firstTime = true;
            foreach (var seat in seats)
            {
                if (!firstTime)
                {
                    sb.Append(", ");
                }
                else
                {
                    firstTime = false;
                }

                sb.Append($"\"{seat.SeatNumber}{seat.CoachName}\"");
            }

            sb.Append("]");

            return sb.ToString();
        }

        public async Task<string> ReserveAsync(string trainId, int seatsRequestedCount)
        {
            return AdaptReservation(await _ticketOffice.Reserve(new TrainId(trainId), new SeatsRequested(seatsRequestedCount)));
        }
    }
}