using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Handlers.Query;
using Mc2.CrudTest.Application.Models.Queries;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.ModelsRepository;

namespace Mc2.CrudTest.Application.UnitTests;

public class ReadQueryHandlerTests
{

    private readonly ReadCustomerQueryHandler _queryHandler;
    private ICustomerRepository _customerRepository;
    public ReadQueryHandlerTests()
    {
        _customerRepository = new CustomerInMemoryRepository();
        _queryHandler = new ReadCustomerQueryHandler(_customerRepository);
    }


    [Theory]
    [InlineData("1232", false)]
    [InlineData("ali@yahoo.com", true)]
    void Should_Validate_Right_Input(string email, bool valid)
    {
      Assert.Equal(_queryHandler.Validate(new ReadCustomerQuery{Email = email}),valid);
    }


    [Theory]
    [InlineData("mail@yahoo.com", ResponseType.NotFound)]
    [InlineData("m@yahoo.com", ResponseType.Success)]
    void Should_Return_Right_value(string email, ResponseType responseType)
    {
        _customerRepository.Create(new Customer{Email ="m@yahoo.com", Id = 1});
        
        Assert.Equal(_queryHandler.Handle(new ReadCustomerQuery{Email = email},CancellationToken.None).Result.ResponseType,responseType);
        
    }
}