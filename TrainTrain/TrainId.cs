using System.Collections.Generic;
using Value;

namespace TrainTrain.Domain
{
    public class TrainId: ValueType<TrainId>
    {
        public string Id { get; }

        public TrainId(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id;
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {Id};
        }
    }
}