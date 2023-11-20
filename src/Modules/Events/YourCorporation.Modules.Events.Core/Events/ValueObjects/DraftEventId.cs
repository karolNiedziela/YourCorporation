using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YourCorporation.Modules.Events.Core.Events.ValueObjects
{
    internal class DraftEventId
    {
        public Guid Value { get; }

        public DraftEventId()
        {
            Value = Guid.NewGuid();
        }

        public DraftEventId(Guid value)
        {
            Value = value;
        }
    }
}
