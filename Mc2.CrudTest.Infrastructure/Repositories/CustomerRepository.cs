using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Domain.Interfaces;
using Mc2.CrusTest.Infrastructure.Persistence;

namespace Mc2.CrusTest.Infrastructure.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly AppDbContext _context;

        public CustomerRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Customer customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Customer customer)
        {
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var customer = await _context.Customers.FindAsync(id);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _context.Customers.FindAsync(id);
        }

        public bool IsCustomerUnique(string firstName, string lastName, DateTime dateOfBirth)
        {
            return !_context.Customers.Any(c => c.FirstName == firstName && c.LastName == lastName && c.DateOfBirth == dateOfBirth);
        }

        public bool IsEmailUnique(string email)
        {
            return !_context.Customers.Any(c => c.Email == email);
        }
    }

}
