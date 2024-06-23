using FluentValidation;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Commands;
using Mc2.CrusTest.Application.Customers.Handlers;
using Moq;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class UpdateCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _repositoryMock;
        private UpdateCustomerCommandHandler _handler;
        private Mock<IEventStore> _eventStoreMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _eventStoreMock = new Mock<IEventStore>();
            _handler = new UpdateCustomerCommandHandler(_repositoryMock.Object, _eventStoreMock.Object);
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenCustomerIsNotFound()
        {
            var command = new UpdateCustomerCommand
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(command.Id)).ReturnsAsync((Customer)null);

            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("Customer not found."));
        }
    }
}
