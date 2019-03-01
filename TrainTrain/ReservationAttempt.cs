using System.Collections.Generic;
using System.Linq;

namespace TrainTrain.Domain
{
    public class ReservationAttempt
    {
        public string TrainId { get; }
        public List<Seat> Seats { get; private set; }
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
            List<Seat> seats = new List<Seat>();
            foreach (var seat in Seats)
            {
                seats.Add(new Seat(seat.CoachName, seat.SeatNumber, BookingReference));
            }

            Seats = seats;
        }

        public Reservation Confirm()
        {
            return new Reservation(TrainId, BookingReference, Seats);
        }
    }
}