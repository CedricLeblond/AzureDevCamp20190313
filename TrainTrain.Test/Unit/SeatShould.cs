using NFluent;
using NUnit.Framework;
using TrainTrain.Domain;

namespace TrainTrain.Test.Unit
{
    internal class SeatShould
    {
        [Test]
        public void Be_value_object()
        {
            var seat = new Seat("A", 1);
            var sameSeat = new Seat("A", 1);

            Check.That(seat).IsEqualTo(sameSeat);
        }
    }
}
