using System;
using System.Collections.Generic;
using System.Linq;
using Value;
using Value.Shared;

namespace TrainTrain.Domain
{
    public class Train : ValueType<Train>
    {
        private readonly Dictionary<string, Coach> _coaches;

        public Train(TrainId trainId, Dictionary<string, Coach> coaches)
        {
            TrainId = trainId;
            _coaches = coaches;
        }

        private int GetMaxSeat()
        {
            return Seats.Count;
        }

        private int ReservedSeats
        {
            get { return Seats.Count(s => !s.IsAvailable()); }
        }

        private TrainId TrainId { get; }
        public IReadOnlyDictionary<string, Coach> Coaches => _coaches;

        private List<Seat> Seats
        {
            get { return Coaches.Values.SelectMany(c => c.Seats).ToList(); }
        }

        public bool DoesNotExceedOverallTrainCapacity(SeatsRequested seatsRequested)
        {
            return ReservedSeats + seatsRequested.Count <= Math.Floor(ThresholdManager.GetMaxRes() * GetMaxSeat());
        }

        public ReservationAttempt BuildReservationAttempt(SeatsRequested seatsRequested)
        {
            foreach (var coach in Coaches.Values)
            {
                var reservationAttempt = coach.BuildReservationAttempt(TrainId, seatsRequested);
                if (reservationAttempt.IsFulFilled())
                    return reservationAttempt;
            }

            return new ReservationAttemptFailure(TrainId, seatsRequested);
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {TrainId, new DictionaryByValue<string, Coach>(_coaches)};
        }
    }
}