using System;
using System.Collections.Generic;
using System.Linq;

namespace TrainTrain
{
    public class Train
    {
        public Train(string trainId, List<Seat> seats)
        {
            TrainId = trainId;
            Seats = seats;
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
        public List<Seat> Seats { get; }

        public bool DoesNotExceedOverallTrainCapacity(int seatsRequestedCount)
        {
            return ReservedSeats + seatsRequestedCount <= Math.Floor(ThresholdManager.GetMaxRes() * GetMaxSeat());
        }

        public ReservationAttempt BuildReservationAttempt(int seatsRequestedCount)
        {
            var availableSeats = Seats.Where(s => s.IsAvailable()).Take(seatsRequestedCount);

            return new ReservationAttempt(TrainId, seatsRequestedCount, availableSeats);
        }
    }
}