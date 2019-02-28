using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainTrain
{
    public class Train
    {
        public Train(string trainId, Dictionary<string, Coach> coaches)
        {
            TrainId = trainId;
            Coaches = coaches;
        }

        private int GetMaxSeat()
        {
            return Seats.Count;
        }

        private int ReservedSeats
        {
            get { return Seats.Count(s => !string.IsNullOrEmpty(s.BookingRef)); }
        }

        public string TrainId { get; }
        public Dictionary<string, Coach> Coaches { get; }
        public List<Seat> Seats
        {
            get { return Coaches.Values.SelectMany(c => c.Seats).ToList(); }
        }

        public bool DoesNotExceedTrainMaxCapacity(int seatsRequestedCount)
        {
            return ReservedSeats + seatsRequestedCount <= Math.Floor(ThresholdManager.GetMaxRes() * GetMaxSeat());
        }

        public ReservationAttempt BuildReservationAttempt(int seatsRequestedCount)
        {

            foreach (var coach in Coaches.Values)
            {
                var reservationAttempt = coach.BuildReservationAttempt(TrainId, seatsRequestedCount);
                if (reservationAttempt.IsFulFilled())
                    return reservationAttempt;
            }

            return new ReservationAttemptFailure(TrainId, seatsRequestedCount);
        }
    }

    public class Coach
    {
        public List<Seat> Seats { get; } = new List<Seat>();
        public string CoachName { get; }

        public Coach(string coachName)
        {
            CoachName = coachName;
        }

        public void AddSeat(Seat seat)
        {
            Seats.Add(seat);
        }

        public ReservationAttempt BuildReservationAttempt(string trainId, int seatsRequestedCount)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable()).Take(seatsRequestedCount);

            return new ReservationAttempt(trainId, seatsRequestedCount, availableSeats);
        }
    }
}