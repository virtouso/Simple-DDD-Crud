using System.Data;
using Mc2.CrudTest.Application.Basic;
using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Models;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.ModelsRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Mc2.CrudTest.Application.Handlers.Command;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, MetaResponse<bool>>
    , IRequestValidator<UpdateCustomerCommand, MetaResponse<bool>>
{
    private ICustomerRepository _customerrepository;

    public UpdateCustomerCommandHandler(ICustomerRepository customerrepository)
    {
        _customerrepository = customerrepository;
    }


    public async Task<MetaResponse<bool>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var validationResult = Validate(request);
        if (!validationResult.Result)
            return validationResult;


        var result = _customerrepository.Update(new Customer
        {
            BankAccountNumber = request.BankAccountNumber
            , FirstName = request.FirstName
            , LastName = request.LastName
            , PhoneNumber = request.PhoneNumber
            ,DateOfBirth = request.DateOfBirth,
            Email = request.Email
        });

        if (!result)
            return new MetaResponse<bool> { Result = false, ResponseType = ResponseType.NotFound, Message = "record not found"};

        return new MetaResponse<bool> { ResponseType = ResponseType.Success, Result = true};
    }

    public MetaResponse<bool> Validate(UpdateCustomerCommand request)
    {
        UpdateCustomerCommand.Validator validator = new();

        if (!validator.Validate(request).IsValid)
            return new MetaResponse<bool> { ResponseType = ResponseType.BadInput, Message = "invalid input sent", Result = false };

 


        var emailResult = _customerrepository.ReadByEmail(request.Email);
        if (emailResult == null)
            return new MetaResponse<bool> { ResponseType = ResponseType.EmailExist, Message = "email exist", Result = false };


        return new MetaResponse<bool> { ResponseType = ResponseType.Success, Result = true };
    }
}