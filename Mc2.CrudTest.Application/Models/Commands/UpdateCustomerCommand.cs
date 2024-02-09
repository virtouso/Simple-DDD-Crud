﻿using FluentValidation;
using Mc2.CrudTest.Application.Basic;
using MediatR;

namespace Mc2.CrudTest.Application.Models.Commands;

public class UpdateCustomerCommand:IRequest<MetaResponse<bool>>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
    public string BankAccountNumber { get; set; }


    public class Validator : AbstractValidator<UpdateCustomerCommand>
    {
        public Validator()
        {
            RuleFor(x => x.FirstName).NotEmpty().Length(3,10);
            RuleFor(x => x.LastName).NotEmpty().Length(3,10);
            RuleFor(x => x.BankAccountNumber).NotEmpty().Length(10, 15);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.DateOfBirth).Must(x => x.Year > 1920 && x.Year < DateTime.UtcNow.Year - 18);
            RuleFor(x => x.PhoneNumber).NotEmpty().Must(x =>
            {
                var phoneNumberUtil = PhoneNumbers.PhoneNumberUtil.GetInstance();
                var phoneNumber = phoneNumberUtil.Parse(x, "US");
                return phoneNumberUtil.IsValidNumber(phoneNumber);
            });
        }
    }
}