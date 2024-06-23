using FluentValidation;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Cutomers.Commands;
using MediatR;

namespace Mc2.CrusTest.Application.Customers.Handlers
{
    public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, Guid>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEventStore _eventStore;

        public CreateCustomerCommandHandler(ICustomerRepository repository, IEventStore eventStore)
        {
            _repository = repository;
            _eventStore = eventStore;
        }

        public async Task<Guid> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
        {
            if (! _repository.IsCustomerUnique(request.FirstName, request.LastName, request.DateOfBirth))
            {
                throw new ValidationException("A customer with the same first name, last name, and date of birth already exists.");
            }

            if (!_repository.IsEmailUnique(request.Email))
            {
                throw new ValidationException("Email must be unique.");
            }

            var customer = new Customer
            {
                Id = Guid.NewGuid(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                BankAccountNumber = request.BankAccountNumber
            };

            await _repository.AddAsync(customer);

            var customerCreatedEvent = new CustomerCreatedEvent
            {
                Id = customer.Id,
                OccurredOn = DateTime.UtcNow,
                Customer = customer
            };

            await _eventStore.SaveAsync(customerCreatedEvent);

            return customer.Id;
        }
    }
}
