using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TrainTrain
{
    public class WebTicketManager
    {
        private readonly ITrainDataService _trainDataService;
        private readonly IBookingReferenceService _bookingReferenceService;
        private const string UriTrainDataService = "http://railway-traindataservice";
        private const string UriBookingReferenceService = "http://railway-bookingreferenceservice";

        public WebTicketManager():this(new TrainDataServiceAdapter(UriTrainDataService), new BookingReferenceServiceAdapter(UriBookingReferenceService))
        {
            
        }
        public WebTicketManager(ITrainDataService trainDataService, IBookingReferenceService bookingReferenceService)
        {
            _trainDataService = trainDataService;
            _bookingReferenceService = bookingReferenceService;
        }

        public async Task<string> Reserve(string trainId, int seatsRequestedCount)
        {
            var jsonTrain = await _trainDataService.GetTrain(trainId);

            var train = new Train(jsonTrain);

            if ((train.ReservedSeats + seatsRequestedCount) <= Math.Floor(ThresholdManager.GetMaxRes() * train.GetMaxSeat()))
            {
                var availableSeats = new List<Seat>();
                // find seats to reserve
                for (int index = 0, i = 0; index < train.Seats.Count; index++)
                {
                    var seat = train.Seats[index];
                    if (seat.BookingRef == "")
                    {
                        i++;
                        if (i <= seatsRequestedCount)
                        {
                            availableSeats.Add(seat);
                        }
                    }
                }

                if (availableSeats.Count == seatsRequestedCount)
                {
                    var bookingReference = await _bookingReferenceService.GetBookingReference();

                    foreach (var availableSeat in availableSeats)
                    {
                        availableSeat.BookingRef = bookingReference;
                    }

                    await _trainDataService.BookSeats(trainId, bookingReference, availableSeats);

                    return $"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"{bookingReference}\", \"seats\": {DumpSeats(availableSeats)}}}";
                    
                }
            }
            return $"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"\", \"seats\": []}}";
        }

        private static string DumpSeats(IEnumerable<Seat> seats)
        {
            var sb = new StringBuilder("[");

            var firstTime = true;
            foreach (var seat in seats)
            {
                if (!firstTime)
                {
                    sb.Append(", ");
                }
                else
                {
                    firstTime = false;
                }

                sb.Append($"\"{seat.SeatNumber}{seat.CoachName}\"");
            }

            sb.Append("]");

            return sb.ToString();
        }
    }
}