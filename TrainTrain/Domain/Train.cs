using System;
using System.Collections.Generic;
using System.Linq;
using Value;

namespace TrainTrain.Domain
{
    public class Train: ValueType<Train>
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

        private string TrainId { get; }
        private Dictionary<string, Coach> Coaches { get; }

        private List<Seat> Seats
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

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {TrainId, new Dictionary<string, Coach>(Coaches)};
        }
    }
}