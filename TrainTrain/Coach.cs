using System;
using System.Collections.Generic;
using System.Linq;
using Value;

namespace TrainTrain.Domain
{
    public class Coach : ValueType<Coach>
    {
        public List<Seat> Seats { get; }
        public string CoachName { get; }

        public Coach(string coachName):this(coachName, new List<Seat>())
        {
        }

        public Coach(string coachName, List<Seat> seats)
        {
            CoachName = coachName;
            Seats = seats;
        }

        public Coach AddSeat(Seat seat)
        {
            return new Coach(seat.CoachName, new List<Seat>(Seats) { seat });
        }

        public ReservationAttempt BuildReservationAttempt(TrainId trainId, SeatsRequested seatsRequested)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable()).Take(seatsRequested.Count);

            return new ReservationAttempt(trainId, seatsRequested, availableSeats);
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {CoachName, new ListByValue<Seat>(Seats)};
        }
    }
}