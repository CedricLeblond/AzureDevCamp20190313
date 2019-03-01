using System.Threading.Tasks;

namespace TrainTrain.Domain
{
    public interface ITicketOffice
    {
        Task<Reservation> Reserve(TrainId trainId, SeatsRequested seatsRequested);
    }
}