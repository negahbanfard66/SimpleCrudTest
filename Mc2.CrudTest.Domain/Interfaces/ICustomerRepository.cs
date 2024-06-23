using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Domain.Interfaces
{
    public interface ICustomerRepository
    {
        Task AddAsync(Customer customer);
        Task UpdateAsync(Customer customer);
        Task DeleteAsync(Guid id);
        Task<Customer> GetByIdAsync(Guid id);
        bool IsEmailUnique(string email);
        bool IsCustomerUnique(string firstName, string lastName, DateTime dateOfBirth);
    }
}
