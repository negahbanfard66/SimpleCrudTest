using MediatR;

namespace Mc2.CrusTest.Application.Customers.Commands
{
    public class DeleteCustomerCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}
