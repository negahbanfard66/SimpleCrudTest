using Mc2.CrusTest.Application.Cutomers.Commands;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class CreateCustomerCommandHandlerTests
    {

        [SetUp]
        public void SetUp() { }

        [Test]
        public void Handle_ShouldCallAdd_WhenCommandIsValid()
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
        }
    }
}
