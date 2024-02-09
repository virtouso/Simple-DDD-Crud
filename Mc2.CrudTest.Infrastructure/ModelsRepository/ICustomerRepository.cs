using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Infrastructure.ModelsRepository;

public interface ICustomerRepository: IRepository<Customer>
{
    Customer ReadByEmail(string Email);
    Customer ReadByNameFamilyBirthDate(string firstName,string lastName, DateTime dateOfBirth);
}