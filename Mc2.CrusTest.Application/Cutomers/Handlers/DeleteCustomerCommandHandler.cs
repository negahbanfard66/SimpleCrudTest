using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Commands;
using MediatR;

namespace Mc2.CrusTest.Application.Customers.Handlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEventStore _eventStore;

        public DeleteCustomerCommandHandler(ICustomerRepository repository, IEventStore eventStore)
        {
            _repository = repository;
            _eventStore = eventStore;
        }

        public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);

            var customerDeletedEvent = new CustomerDeletedEvent
            {
                Id = request.Id,
                OccurredOn = DateTime.UtcNow,
            };

            await _eventStore.SaveAsync(customerDeletedEvent);
        }
    }
}
