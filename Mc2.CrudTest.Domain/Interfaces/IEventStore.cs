using Mc2.CrudTest.Domain.Events;

namespace Mc2.CrudTest.Domain.Interfaces
{
    public interface IEventStore
    {
        Task SaveAsync(Event @event);
        Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId);
    }
}
