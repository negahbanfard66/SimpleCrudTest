using Mc2.CrudTest.Domain.Events;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Commands;
using Mc2.CrusTest.Application.Customers.Handlers;
using Moq;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class DeleteCustomerCommandHandlerTests
    {
        private Mock<ICustomerRepository> _repositoryMock;
        private DeleteCustomerCommandHandler _handler;
        private Mock<IEventStore> _eventStoreMock;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _eventStoreMock = new Mock<IEventStore>();
            _handler = new DeleteCustomerCommandHandler(_repositoryMock.Object, _eventStoreMock.Object);
        }

        [Test]
        public async Task Handle_ShouldDeleteCustomer_WhenCustomerExists()
        {
            var command = new DeleteCustomerCommand
            {
                Id = Guid.NewGuid()
            };

            _repositoryMock.Setup(r => r.DeleteAsync(command.Id)).Returns(Task.CompletedTask);

            await _handler.Handle(command, CancellationToken.None);

            _repositoryMock.Verify(r => r.DeleteAsync(command.Id), Times.Once);
            _eventStoreMock.Verify(e => e.SaveAsync(It.IsAny<CustomerDeletedEvent>()), Times.Once);
        }
    }
}
