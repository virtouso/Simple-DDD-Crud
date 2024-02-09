using System.Data;
using Mc2.CrudTest.Application.Handlers.Command;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.ModelsRepository;

namespace Mc2.CrudTest.Application.UnitTests;

public class CreateCommandHandlerTests
{
    private readonly CreateCustomerCommandHandler _commandHandler;
    private ICustomerRepository _customerRepository;

    public CreateCommandHandlerTests()
    {
        _customerRepository = new CustomerInMemoryRepository();
        _commandHandler = new CreateCustomerCommandHandler(_customerRepository);
    }


    [Fact]
    public void Should_Invalidate_Duplicate_Email()
    {
        //arrange
        _customerRepository.Create(new Customer
        {
            Id = 1
            , FirstName = "c"
            , Email = "c@y.c"
            , BankAccountNumber = "111222333"
            , LastName = "het"
            , PhoneNumber = "123123123"
            , DateOfBirth = new DateTime(1990, 2, 2)
        });

        //act
        var result = _commandHandler.Validate(new CreateCustomerCommand
        {
            BankAccountNumber = "123123123"
            , PhoneNumber = "123123123"
            , DateOfBirth = new DateTime(1991, 2, 2)
            , FirstName = "a"
            , Email = "c@y.c"
            , LastName = "h"
        });

        //assert

        Assert.Equal(result.Result, false);
    }

    [Fact]
    public void Should_Invalidate_Duplicate_Name_BirthDate()
    {
        //arrange
        _customerRepository.Create(new Customer
        {
            Id = 1
            , FirstName = "c"
            , Email = "cc@y.c"
            , BankAccountNumber = "111222333"
            , LastName = "het"
            , PhoneNumber = "123123123"
            , DateOfBirth = new DateTime(1990, 2, 2)
        });

        //act
        var result = _commandHandler.Validate(new CreateCustomerCommand
        {
            BankAccountNumber = "123123123"
            , PhoneNumber = "123123123"
            , DateOfBirth = new DateTime(1990, 2, 2)
            , FirstName = "c"
            , Email = "c@y.c"
            , LastName = "het"
        });

        //assert

        Assert.Equal(result.Result, false);
    }

    [Theory]
    [InlineData("", "ali", "2024561111", "a@y.com", "1231231231",false)]
    [InlineData("ali", "ali", "2024561111", "a@y.com", "1231231231",true)]
    [InlineData("ali", "ali", "2024561111", "ay.com", "1231231231",false)]
    [InlineData("ali", "ali", "123123123", "a@y.com", "1231231231",false)]
    void Should_Validate_Right_Input(string name, string family, string phoneNumber, string email, string bankNumber, bool validate)
    {
        //arrange
        //act
       var result= _commandHandler.Validate(new CreateCustomerCommand
        {
            BankAccountNumber = bankNumber
            , PhoneNumber = phoneNumber
            , DateOfBirth = new DateTime(1990, 2, 2)
            , FirstName = name
            , Email = email
            , LastName = family
        });
        
        
        //assert
        Assert.Equal(result.Result,validate);
    }


    [Theory]
    [InlineData("ali", "ali", "2024561111", "a@y.com", "1231231231",false)]
    public void Should_Create_When_Right(string name, string family, string phoneNumber, string email, string bankNumber, bool validate)
    {
        //arrage
        //act
        var result= _commandHandler.Handle(new CreateCustomerCommand
        {
            BankAccountNumber = bankNumber
            , PhoneNumber = phoneNumber
            , DateOfBirth = new DateTime(1990, 2, 2)
            , FirstName = name
            , Email = email
            , LastName = family
        }, CancellationToken.None);
        
        //assert
        Assert.True(_customerRepository.ReadByEmail(email)!=null);
    }

   
}