using System.Collections.Generic;
using TrainTrain.Infra;

namespace TrainTrain.Domain
{
    public class ReservationFailure : Reservation
    {
        public ReservationFailure(TrainId trainId):base(trainId, new BookingReference(), new List<Seat>())
        {
            
        }
    }
}