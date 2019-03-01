using System.Collections.Generic;
using Value;

namespace TrainTrain.Domain
{
    public class Seat : ValueType<Seat>
    {
        public string CoachName { get; }
        public int SeatNumber { get; }
        public BookingReference BookingReference { get; }

        public Seat(string coachName, int seatNumber, BookingReference bookingReference)
        {
            CoachName = coachName;
            SeatNumber = seatNumber;
            BookingReference = bookingReference;
        }

        public Seat(string coachName, int seatNumber):this(coachName, seatNumber, new BookingReference(string.Empty))
        {
        }

        public bool IsAvailable()
        {
            return string.IsNullOrEmpty(BookingReference.Id);
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {CoachName, SeatNumber, BookingReference};
        }
    }
}