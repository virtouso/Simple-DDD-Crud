using Mc2.CrudTest.Application.Basic;
using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Models;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Application.Models.Queries;
using Mc2.CrudTest.Domain.Entities;
using Mc2.CrudTest.Infrastructure.Models;
using Mc2.CrudTest.Infrastructure.ModelsRepository;
using MediatR;

namespace Mc2.CrudTest.Application.Handlers.Query;

public class ReadCustomerQueryHandler : IRequestHandler<ReadCustomerQuery, MetaResponse<Customer>>, IRequestValidator<ReadCustomerQuery, bool>
{

    private ICustomerRepository _customerRepository;

    public ReadCustomerQueryHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<MetaResponse<Customer>> Handle(ReadCustomerQuery request, CancellationToken cancellationToken)
    {
        var valid = Validate(request);
        if (!valid)
            return new MetaResponse<Customer>{Message = "invalid input", ResponseType = ResponseType.BadInput};

        var result = _customerRepository.ReadByEmail(request.Email);
        if (result == null)
            return new MetaResponse<Customer> {ResponseType = ResponseType.NotFound, Message = "customer not found"};

        return new MetaResponse<Customer> {Result = result, ResponseType = ResponseType.Success};
    }

    public bool Validate(ReadCustomerQuery request)
    {
        ReadCustomerQuery.Validator validator = new();
        return validator.Validate(request).IsValid;
    }
}