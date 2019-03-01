using System.Collections.Generic;

namespace TrainTrain.Domain
{
    public class ReservationAttemptFailure : ReservationAttempt
    {
        public ReservationAttemptFailure(TrainId trainId, SeatsRequested seatsRequested): base(trainId, seatsRequested, new List<Seat>())
        {
        }
    }
}