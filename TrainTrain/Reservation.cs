using System.Collections.Generic;
using TrainTrain.Infra;

namespace TrainTrain.Domain
{
    public class Reservation
    {
        public TrainId TrainId { get; }
        public BookingReference BookingReference { get; }
        public List<Seat> Seats { get; }

        public Reservation(TrainId trainId, BookingReference bookingReference, List<Seat> seats)
        {
            TrainId = trainId;
            BookingReference = bookingReference;
            Seats = seats;
        }
    }
}