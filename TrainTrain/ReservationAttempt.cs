using System.Collections.Generic;
using System.Linq;

namespace TrainTrain
{
    public class ReservationAttempt
    {
        public string TrainId { get; }
        public List<Seat> Seats { get; }
        public string BookingReference { get; private set; }
        public int SeatsRequestedCount { get; }

        public ReservationAttempt(string trainId, int seatsRequestedCount, IEnumerable<Seat> seats)
        {
            TrainId = trainId;
            SeatsRequestedCount = seatsRequestedCount;
            Seats = seats.ToList();
        }

        public bool IsFulFilled()
        {
            return Seats.Count == SeatsRequestedCount;
        }

        public void AssignBookingReference(string bookingReference)
        {
            BookingReference = bookingReference;
            foreach (var seat in Seats)
            {
                seat.BookingRef = bookingReference;
            }
        }

        public Reservation Confirm()
        {
            return new Reservation(TrainId, BookingReference, Seats);
        }
    }
}