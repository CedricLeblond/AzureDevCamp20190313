using System.Collections.Generic;
using Value;

namespace TrainTrain.Domain
{
    public class SeatsRequested: ValueType<SeatsRequested>
    {
        public int Count { get; }

        public SeatsRequested(int seatsRequestedCount)
        {
            Count = seatsRequestedCount;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] { Count };
        }
    }
}