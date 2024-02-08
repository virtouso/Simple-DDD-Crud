using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Infrastructure.ModelsRepository;

// this class is used for unit testing
public class CustomerInMemoryRepository : ICustomerRepository
{
    private List<Customer> _customers = new();

    public CustomerInMemoryRepository()
    {
        //adding some mock data
    }

    public bool Create(Customer item)
    {
        if (_customers.FirstOrDefault(x => x.Id == item.Id) != null)
            return false;
        _customers.Add(item);
        return true;
    }

    public Customer Read(Int64 id)
    {
        return _customers.FirstOrDefault(x => x.Id == id);
    }

    public bool Update(Customer item)
    {
        var result = _customers.FirstOrDefault(x => x.Id == item.Id);

        if (result == null) return false;

        result.Email = item.Email;
        result.FirstName = item.Email;
        result.LastName = item.LastName;
        result.BankAccountNumber = item.BankAccountNumber;
        result.PhoneNumber = item.PhoneNumber;
        result.DateOfBirth = item.DateOfBirth;

        return true;
    }

    public bool Delete(Int64 id)
    {
        var result = _customers.FirstOrDefault(x => x.Id == id);
        if (result == null) return false;

        _customers.Remove(result);
        return true;
    }
}