using Microsoft.AspNetCore.Mvc;
using Railway.TrainDataService.Models;

namespace Railway.TrainDataService.Controllers
{
    [Route("api/data_for_train")]
    public class TrainController : Controller
    {
        private readonly IProvideTrain trainProvider;

        public TrainController(IProvideTrain trainProvider)
        {
            this.trainProvider = trainProvider;
        }

        // GET api/data_for_train/5FSdR
        [HttpGet("{trainId}")]
        public string Get(string trainId)
        {
            return this.trainProvider.GetTrain(trainId).ToString();
        }
    }
}
