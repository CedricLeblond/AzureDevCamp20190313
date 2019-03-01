using System.Threading.Tasks;
using NFluent;
using NSubstitute;
using NUnit.Framework;
using TrainTrain.Domain;
using TrainTrain.Infra;

namespace TrainTrain.Test.Acceptance
{
    public class TrainTrainShould
    {
        private readonly TrainId _trainId = new TrainId("9043-2019-03-13");
        private readonly BookingReference _bookingReference = new BookingReference("75bcd15");

        [Test]
        public void Reserve_seats_when_train_is_empty()
        {
            const int seatsRequestedCount = 3;

            var trainDataService = BuildTrainDataService(_trainId, TrainTopologyGenerator.With_10_available_seats());
            var bookingReferenceService = BuildBookingReferenceService(_bookingReference);

            ITicketOffice ticketOffice = new TicketOffice(trainDataService, bookingReferenceService);
            var seatsReservationAdapter = new SeatsReservationAdapter(ticketOffice);
            var jsonReservation = seatsReservationAdapter.ReserveAsync(_trainId.Id, seatsRequestedCount).Result;

            Check.That(jsonReservation)
                .IsEqualTo(
                    $"{{\"train_id\": \"{_trainId}\", \"booking_reference\": \"{_bookingReference}\", \"seats\": [\"1A\", \"2A\", \"3A\"]}}");
        }

        [Test]
        public void Not_reserve_seats_when_it_exceed_max_capacity_threshold()
        {
            var seatsRequestedCount = new SeatsRequested(3);

            var trainDataService =
                BuildTrainDataService(_trainId, TrainTopologyGenerator.With_10_seats_and_6_already_reserved());
            var bookingReferenceService = BuildBookingReferenceService(_bookingReference);

            var ticketOffice = new TicketOffice(trainDataService, bookingReferenceService);
            var reservation = ticketOffice.Reserve(_trainId, seatsRequestedCount).Result;

            Check.That(SeatsReservationAdapter.AdaptReservation(reservation))
                .IsEqualTo($"{{\"train_id\": \"{_trainId}\", \"booking_reference\": \"\", \"seats\": []}}");
        }

        [Test]
        public void Reserve_all_seats_in_the_same_coach()
        {
            var seatsRequestedCount = new SeatsRequested(2);

            var trainDataService = BuildTrainDataService(_trainId,
                TrainTopologyGenerator.With_2_coaches_and_9_seats_already_reserved_in_the_first_coach());
            var bookingReferenceService = BuildBookingReferenceService(_bookingReference);

            var ticketOffice = new TicketOffice(trainDataService, bookingReferenceService);
            var reservation = ticketOffice.Reserve(_trainId, seatsRequestedCount).Result;

            Check.That(SeatsReservationAdapter.AdaptReservation(reservation))
                .IsEqualTo(
                    $"{{\"train_id\": \"{_trainId}\", \"booking_reference\": \"{_bookingReference}\", \"seats\": [\"1B\", \"2B\"]}}");
        }

        private static IBookingReferenceService BuildBookingReferenceService(BookingReference bookingReference)
        {
            var bookingReferenceService = Substitute.For<IBookingReferenceService>();
            bookingReferenceService.GetBookingReference().Returns(Task.FromResult(bookingReference));
            return bookingReferenceService;
        }

        private static ITrainDataService BuildTrainDataService(TrainId trainId, string trainTopology)
        {
            var trainDataService = Substitute.For<ITrainDataService>();
            trainDataService.GetTrain(trainId)
                .Returns(Task.FromResult(new Train(trainId,
                    TrainDataServiceAdapter.AdaptTrainTopology(trainTopology))));
            return trainDataService;
        }
    }
}