using System;
using System.Collections.Generic;
using System.Linq;
using Value;
using Value.Shared;

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
        public Dictionary<string, Coach> Coaches { get; }

        private List<Seat> Seats
        {
            get { return Coaches.Values.SelectMany(c => c.Seats).ToList(); }
        }

        public bool DoesNotExceedOverallTrainCapacity(int seatsRequestedCount)
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
            return new object[] {TrainId, new DictionaryByValue<string, Coach>(Coaches)};
        }
    }
}