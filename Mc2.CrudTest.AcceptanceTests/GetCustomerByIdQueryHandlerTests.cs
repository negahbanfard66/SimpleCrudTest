using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Application.Customers.Handlers;
using Mc2.CrusTest.Application.Cutomers.Queries;
using Moq;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class GetCustomerByIdQueryHandlerTests
    {
        private Mock<ICustomerRepository> _repositoryMock;
        private GetCustomerByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _repositoryMock = new Mock<ICustomerRepository>();
            _handler = new GetCustomerByIdQueryHandler(_repositoryMock.Object);
        }

        [Test]
        public async Task Handle_ShouldReturnCustomer_WhenCustomerExists()
        {
            var query = new GetCustomerByIdQuery
            {
                Id = Guid.NewGuid()
            };

            var customer = new Customer
            {
                Id = query.Id,
                FirstName = "John",
                LastName = "Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                PhoneNumber = "+1234567890",
                Email = "john.doe@example.com",
                BankAccountNumber = "123456789"
            };

            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync(customer);

            var result = await _handler.Handle(query, CancellationToken.None);

            Assert.NotNull(result);
            Assert.AreEqual(query.Id, result.Id);
        }

        [Test]
        public async Task Handle_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            var query = new GetCustomerByIdQuery
            {
                Id = Guid.NewGuid()
            };
            Customer? result = null;
            _repositoryMock.Setup(r => r.GetByIdAsync(query.Id)).ReturnsAsync((Customer)null);
            result = await _handler.Handle(query, CancellationToken.None);
            Assert.IsNull(result);
        }
    }
}
