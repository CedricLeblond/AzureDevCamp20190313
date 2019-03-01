using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TrainTrain.Domain;

namespace TrainTrain.Infra
{
    public class BookingReferenceServiceAdapter : IBookingReferenceService
    {
        private readonly string _uriBookingReferenceService;

        public BookingReferenceServiceAdapter(string uriBookingReferenceService)
        {
            _uriBookingReferenceService = uriBookingReferenceService;
        }

        public async Task<string> GetBookingReference()
        {
            using (var client = new HttpClient())
            {
                var value = new MediaTypeWithQualityHeaderValue("application/json");
                client.BaseAddress = new Uri(_uriBookingReferenceService);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(value);

                // HTTP GET
                var response = await client.GetAsync("/booking_reference");
                response.EnsureSuccessStatusCode();
                return await response.Content.ReadAsStringAsync();
            }
        }
    }
}