using System.Threading.Tasks;
using TrainTrain.Infra;

namespace TrainTrain.Domain
{
    public interface IBookingReferenceService
    {
        Task<BookingReference> GetBookingReference();
    }
}