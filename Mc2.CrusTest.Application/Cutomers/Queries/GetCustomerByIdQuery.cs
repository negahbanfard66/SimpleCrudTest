using Mc2.CrudTest.Domain.Entities;
using MediatR;

namespace Mc2.CrusTest.Application.Cutomers.Queries
{
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
