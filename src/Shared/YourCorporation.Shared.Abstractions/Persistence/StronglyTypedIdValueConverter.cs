using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YourCorporation.Shared.Abstractions.Types;

namespace YourCorporation.Shared.Abstractions.Persistence
{
    public class StronglyTypedIdValueConverter<TTypedIdValue> : ValueConverter<TTypedIdValue, Guid>
        where TTypedIdValue : StronglyTypedId
    {
        public StronglyTypedIdValueConverter(ConverterMappingHints mappingHints = null)
            : base(id => id.Value, value => Create(value), mappingHints)
        {
        }

        private static TTypedIdValue Create(Guid id) => Activator.CreateInstance(typeof(TTypedIdValue), id) as TTypedIdValue;
    }
}
