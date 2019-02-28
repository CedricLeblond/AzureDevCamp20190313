using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace TrainTrain
{
    public class WebTicketManager
    {
        private const string UriBookingReferenceService = "http://localhost:51691/";
        private const string UriTrainDataService = "http://localhost:50680";

        public async Task<string> Reserve(string trainId, int seatsRequestedCount)
        {
            var jsonTrain = await GetTrain(trainId);

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
                    string bookingReference;

                    using (var client = new HttpClient())
                    {
                        bookingReference = await GetBookRef(client);
                    }

                    foreach (var availableSeat in availableSeats)
                    {
                        availableSeat.BookingRef = bookingReference;
                    }

                    using (var client = new HttpClient())
                    {
                        var value = new MediaTypeWithQualityHeaderValue("application/json");
                        client.BaseAddress = new Uri(UriTrainDataService);
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(value);

                        // HTTP POST
                        HttpContent resJson = new StringContent(BuildPostContent(trainId, bookingReference, availableSeats), Encoding.UTF8, "application/json");
                        var response = await client.PostAsync("reserve", resJson);

                        response.EnsureSuccessStatusCode();


                        return $"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"{bookingReference}\", \"seats\": {DumpSeats(availableSeats)}}}";
                    }
                }
                else
                {
                    return $"{{\"train_id\": \"{trainId}\", \"booking_reference\": \"\", \"seats\": []}}";
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

        private static string BuildPostContent(string trainId, string bookingRef, IEnumerable<Seat> availableSeats)
        {
            var seats = new StringBuilder("[");
            bool firstTime = true;

            foreach (var s in availableSeats)
            {
                if (!firstTime)
                {
                    seats.Append(", ");
                }
                else
                {
                    firstTime = false;
                }

                seats.Append($"\"{s.SeatNumber}{s.CoachName}\"");
            }
            seats.Append("]");

            var result = $"{{\r\n\t\"train_id\": \"{trainId}\",\r\n\t\"seats\": {seats},\r\n\t\"booking_reference\": \"{bookingRef}\"\r\n}}";

            return result;
        }

        private async Task<string> GetTrain(string trainId)
        {
            using (var client = new HttpClient())
            {
                var value = new MediaTypeWithQualityHeaderValue("application/json");
                client.BaseAddress = new Uri(UriTrainDataService);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(value);

                // HTTP GET
                var response = await client.GetAsync($"api/data_for_train/{trainId}");
                response.EnsureSuccessStatusCode();
               return await response.Content.ReadAsStringAsync();
            }
        }

        private async Task<string> GetBookRef(HttpClient client)
        {
            var value = new MediaTypeWithQualityHeaderValue("application/json");
            client.BaseAddress = new Uri(UriBookingReferenceService);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(value);

            // HTTP GET
            var response = await client.GetAsync("/booking_reference");
            response.EnsureSuccessStatusCode();

            return await response.Content.ReadAsStringAsync();
        }
    }
}