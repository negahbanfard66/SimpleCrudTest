using Mc2.CrudTest.Domain.Entities;
using Mc2.CrusTest.Application.Cutomers.Queries;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class GetCustomerByIdQueryHandlerTests
    {

        [SetUp]
        public void SetUp(){}

        [Test]
        public void Handle_ShouldReturnCustomer_WhenCustomerExists()
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
        }

        [Test]
        public void Handle_ShouldReturnNull_WhenCustomerDoesNotExist()
        {
            var query = new GetCustomerByIdQuery
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
