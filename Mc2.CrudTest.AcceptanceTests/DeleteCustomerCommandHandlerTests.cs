using Mc2.CrusTest.Application.Customers.Commands;
using NUnit.Framework;

namespace Mc2.CrudTest.AcceptanceTests
{
    [TestFixture]
    public class DeleteCustomerCommandHandlerTests
    {

        [SetUp]
        public void SetUp() { }

        [Test]
        public void Handle_ShouldDeleteCustomer_WhenCustomerExists()
        {
            var command = new DeleteCustomerCommand
            {
                Id = Guid.NewGuid()
            };
        }
    }
}
