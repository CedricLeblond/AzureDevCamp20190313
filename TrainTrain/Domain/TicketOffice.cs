using System.Threading.Tasks;

namespace TrainTrain.Domain
{
    public class TicketOffice : ITicketOffice
    {
        private readonly ITrainDataService _trainDataService;
        private readonly IBookingReferenceService _bookingReferenceService;

        public TicketOffice(ITrainDataService trainDataService, IBookingReferenceService bookingReferenceService)
        {
            _trainDataService = trainDataService;
            _bookingReferenceService = bookingReferenceService;
        }

        public async Task<Reservation> Reserve(string trainId, int seatsRequestedCount)
        {
            var train = await _trainDataService.GetTrain(trainId);

            if (train.DoesNotExceedTrainMaxCapacity(seatsRequestedCount))
            {
                var reservationAttempt = train.BuildReservationAttempt(seatsRequestedCount);

                if (reservationAttempt.IsFulFilled())
                {
                    var bookingReference = await _bookingReferenceService.GetBookingReference();

                    reservationAttempt.AssignBookingReference(bookingReference);

                    await _trainDataService.BookSeats(reservationAttempt);

                    return reservationAttempt.Confirm();
                }
            }
            return new ReservationFailure(trainId);
        }
    }
}