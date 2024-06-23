using FluentValidation;
using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Commands;
using MediatR;

namespace Mc2.CrusTest.Application.Customers.Handlers
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand>
    {
        private readonly ICustomerRepository _repository;
        private readonly IEventStore _eventStore;

        public UpdateCustomerCommandHandler(ICustomerRepository repository, IEventStore eventStore)
        {
            _repository = repository;
            _eventStore = eventStore;
        }

        public async Task Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _repository.GetByIdAsync(request.Id);
            if (customer == null)
            {
                throw new ValidationException("Customer not found.");
            }

            if ( _repository.IsCustomerUnique(request.FirstName, request.LastName, request.DateOfBirth) &&
                !(customer.FirstName == request.FirstName && customer.LastName == request.LastName && customer.DateOfBirth == request.DateOfBirth))
            {
                throw new ValidationException("A customer with the same first name, last name, and date of birth already exists.");
            }

            if ( _repository.IsEmailUnique(request.Email) && customer.Email != request.Email)
            {
                throw new ValidationException("Email must be unique.");
            }

            if (customer != null)
            {
                customer.FirstName = request.FirstName;
                customer.LastName = request.LastName;
                customer.DateOfBirth = request.DateOfBirth;
                customer.PhoneNumber = request.PhoneNumber;
                customer.Email = request.Email;
                customer.BankAccountNumber = request.BankAccountNumber;

                await _repository.UpdateAsync(customer);

                var customerUpdatedEvent = new CustomerUpdatedEvent
                {
                    Id = customer.Id,
                    OccurredOn = DateTime.UtcNow,
                    Customer = customer
                };

                await _eventStore.SaveAsync(customerUpdatedEvent);
            }
        }
    }
}
