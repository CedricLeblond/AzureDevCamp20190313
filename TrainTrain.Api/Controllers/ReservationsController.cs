using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TrainTrain.Infra;

namespace TrainTrain.Api.Controllers
{
    [Route("api/[controller]")]
    public class ReservationsController : Controller
    {
        private readonly SeatsReservationAdapter _seatsReservationAdapter;

        public ReservationsController(SeatsReservationAdapter seatsReservationAdapter)
        {
            _seatsReservationAdapter = seatsReservationAdapter;
        }

        [HttpGet]
        public async Task<ActionResult<string>> Get([FromQuery(Name = "trainId")] string trainId,
            [FromQuery(Name = "numberOfSeats")] int numberOfSeats)
        {
            return await _seatsReservationAdapter.ReserveAsync(trainId, numberOfSeats);
        }
    }
}