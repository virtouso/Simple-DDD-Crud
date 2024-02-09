using FluentValidation;
using Mc2.CrudTest.Application.Basic;
using Mc2.CrudTest.Domain.Entities;
using MediatR;

namespace Mc2.CrudTest.Application.Models.Queries;

public class ReadCustomerQuery :IRequest<MetaResponse<Customer>>
{
    public string Email { get; set; }


    public class Validator : AbstractValidator<ReadCustomerQuery>
    {
        public Validator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
        }
    }
}