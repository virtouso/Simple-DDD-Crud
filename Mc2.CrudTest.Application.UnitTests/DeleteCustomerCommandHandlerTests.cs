using Mc2.CrudTest.Application.Handlers.Command;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.ModelsRepository;

namespace Mc2.CrudTest.Application.UnitTests;

public class DeleteCustomerCommandHandlerTests
{
    
    private ICustomerRepository _customerRepository;
    private DeleteCustomerCommandHandler _commandHandler;

    public DeleteCustomerCommandHandlerTests()
    {
        _customerRepository = new CustomerInMemoryRepository();
        _commandHandler = new(_customerRepository);
    }
    
    [Theory]
    [InlineData(1,true)]
    [InlineData(2,false)]
    public void Delete_If_Exist(Int64 id, bool valid)
    {
        _customerRepository.Create(new Customer{Id = 1});
        var result = _commandHandler.Handle(new DeleteCustomerCommand{Id = id}, CancellationToken.None);
        Assert.Equal(result.Result.Result,valid);
    }
}