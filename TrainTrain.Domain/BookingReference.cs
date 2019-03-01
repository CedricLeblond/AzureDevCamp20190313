using System.Collections.Generic;
using Value;

namespace TrainTrain.Domain
{
    public class BookingReference : ValueType<BookingReference>
    {
        public string Id { get; }

        public BookingReference() : this(string.Empty)
        {
        }

        public BookingReference(string id)
        {
            Id = id;
        }

        public override string ToString()
        {
            return Id;
        }

        public bool IsValid()
        {
            return !string.IsNullOrEmpty(Id);
        }

        protected override IEnumerable<object> GetAllAttributesToBeUsedForEquality()
        {
            return new object[] {Id};
        }
    }
}