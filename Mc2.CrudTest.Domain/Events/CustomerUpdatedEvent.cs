using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Domain.Events
{
    public class CustomerUpdatedEvent : Event
    {
        public Customer Customer { get; set; }
    }
}
