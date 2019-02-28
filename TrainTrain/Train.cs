using System.Collections.Generic;
using Newtonsoft.Json;

namespace TrainTrain
{
    public class Train
    {
        public Train(string trainTopology)
        {
            Seats = new List<Seat>();
            //var sample =
            //"{\"seats\": {\"1A\": {\"booking_reference\": \"\", \"seat_number\": \"1\", \"coach\": \"A\"}, \"2A\": {\"booking_reference\": \"\", \"seat_number\": \"2\", \"coach\": \"A\"}}}";

            // Forced to workaround with dynamic parsing since the received JSON is invalid format ;-(
            dynamic parsed = JsonConvert.DeserializeObject(trainTopology);

            foreach (var token in ((Newtonsoft.Json.Linq.JContainer)parsed))
            {
                var allStuffs = ((Newtonsoft.Json.Linq.JObject) ((Newtonsoft.Json.Linq.JContainer) token).First);

                foreach (var stuff in allStuffs)
                {
                    var seat = stuff.Value.ToObject<SeatJsonPoco>();
                    Seats.Add(new Seat(seat.coach, int.Parse(seat.seat_number), seat.booking_reference));
                    if (!string.IsNullOrEmpty(seat.booking_reference))
                    {
                        ReservedSeats++;
                    }
                }
            }
        }

        public int GetMaxSeat()
        {
            return Seats.Count;
        }

        public int ReservedSeats { get; private set; }
        public List<Seat> Seats { get; private set; }
    }
}