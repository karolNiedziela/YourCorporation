using Microsoft.EntityFrameworkCore;

namespace YourCorporation.Shared.Abstractions.Messaging.Inbox
{
    public interface IInboxDbSet
    {
        public DbSet<InboxMessage> Inbox { get; set; }
    }
}
