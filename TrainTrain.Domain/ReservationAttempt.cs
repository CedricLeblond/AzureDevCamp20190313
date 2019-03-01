using System.Collections.Generic;
using System.Linq;

namespace TrainTrain.Domain
{
    public class ReservationAttempt
    {
        public TrainId TrainId { get; }
        public List<Seat> Seats { get; private set; }
        public BookingReference BookingReference { get; private set; }
        public SeatsRequested SeatsRequested { get; }

        public ReservationAttempt(TrainId trainId, SeatsRequested seatsRequestedCount, IEnumerable<Seat> seats)
        {
            TrainId = trainId;
            SeatsRequested = seatsRequestedCount;
            Seats = seats.ToList();
        }

        public bool IsFulFilled()
        {
            return Seats.Count == SeatsRequested.Count;
        }

        public void AssignBookingReference(BookingReference bookingReference)
        {
            BookingReference = bookingReference;
            var seats = new List<Seat>();
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