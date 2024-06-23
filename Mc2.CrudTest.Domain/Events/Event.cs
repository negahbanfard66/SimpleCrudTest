namespace Mc2.CrudTest.Domain.Events
{
    public abstract class Event
    {
        public Guid Id { get; set; }
        public DateTime OccurredOn { get; set; }
    }
}
