using Mc2.CrudTest.Application.Basic;
using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Models;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Infrastructure.Models;
using Mc2.CrudTest.Infrastructure.ModelsRepository;
using Mc2.CrudTest.Infrastructure.ModelsRepository.Events;
using MediatR;

namespace Mc2.CrudTest.Application.Handlers.Command;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, MetaResponse<bool>>
{
    private ICustomerRepository _customerrepository;
    private ICustomerEventsRepository _customerEventsRepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerrepository, ICustomerEventsRepository customerEventsRepository)
    {
        _customerrepository = customerrepository;
        _customerEventsRepository = customerEventsRepository;
    }


    public async Task<MetaResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var getResult = _customerrepository.Read(request.Id);
        var result = _customerrepository.Delete(request.Id);

        if (!result)
            return new MetaResponse<bool> { ResponseType = ResponseType.NotFound, Message = " not found" };

        _customerEventsRepository.Create(new CustomerEvent
        {
            Email = getResult.Email
            , DateOfBirth = getResult.DateOfBirth
            , FirstName = getResult.FirstName
            , LastName = getResult.LastName
            , PhoneNumber = getResult.PhoneNumber
            , BankAccountNumber = getResult.BankAccountNumber
            , EventDate = DateTime.UtcNow
            , EventType = EventType.Delete
        });

        return new MetaResponse<bool> { ResponseType = ResponseType.Success, Result = true };
    }
}