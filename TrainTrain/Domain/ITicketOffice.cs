using System.Threading.Tasks;

namespace TrainTrain.Domain
{
    public interface ITicketOffice
    {
        Task<Reservation> Reserve(string trainId, int seatsRequestedCount);
    }
}