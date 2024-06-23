using FluentValidation;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Handlers;
using Mc2.CrusTest.Application.Cutomers.Commands;
using Moq;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class CreateCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _repositoryMock;
        private CreateCustomerCommandHandler _handler;
        private Mock<IEventStore> _eventStoreMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _eventStoreMock = new Mock<IEventStore>();
            _handler = new CreateCustomerCommandHandler(_repositoryMock.Object, _eventStoreMock.Object);
        }

        [Test]
        public async Task Handle_ShouldCallAddAsync_WhenCommandIsValid()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };

            _repositoryMock.Setup(r => r.IsCustomerUnique(command.FirstName, command.LastName, command.DateOfBirth)).Returns(true);
            _repositoryMock.Setup(r => r.IsEmailUnique(command.Email)).Returns(true);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.AddAsync(It.IsAny<Customer>()), Times.Once);
            _eventStoreMock.Verify(e => e.SaveAsync(It.IsAny<CustomerCreatedEvent>()), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowValidationException_WhenCustomerIsNotUnique()
        {
            var command = new CreateCustomerCommand
            {
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };

            _repositoryMock.Setup(r => r.IsCustomerUnique(command.FirstName, command.LastName, command.DateOfBirth)).Returns(false);
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
            Assert.That(ex.Message, Is.EqualTo("A customer with the same first name, last name, and date of birth already exists."));
        }
    }
}
