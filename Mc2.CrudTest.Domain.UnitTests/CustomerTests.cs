using Mc2.CrudTest.Domain.Entities;

namespace Mc2.CrudTest.Domain.UnitTests;

public class CustomerTests
{
    [Fact]
    public void CreateCustomer_ReturnsValidObject()
    {
        //arrange
        //act
        Customer customer = new Customer();

        //assert
        Assert.NotNull(customer);
        
    }
}