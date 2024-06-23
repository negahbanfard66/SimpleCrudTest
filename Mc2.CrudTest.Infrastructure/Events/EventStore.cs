using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;

namespace Mc2.CrusTest.Infrastructure.Events
{
    public class EventStore : IEventStore
    {
        private readonly List<Event> _events = new List<Event>();

        public async Task SaveAsync(Event @event)
        {
            _events.Add(@event);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Event>> GetEventsAsync(Guid aggregateId)
        {
            var events = _events.Where(e => e.Id == aggregateId);
            return await Task.FromResult(events);
        }
    }
}
