using System.Collections.Generic;
using System.Threading.Tasks;

namespace TrainTrain
{
    public interface ITrainDataService
    {
        Task<string> GetTrain(string trainId);
        Task BookSeats(string trainId, string bookingReference, List<Seat> availableSeats);
    }
}