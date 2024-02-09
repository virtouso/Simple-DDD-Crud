using Mc2.CrudTest.Application.Basic;
using Mc2.CrudTest.Application.Basic.Enums;
using Mc2.CrudTest.Application.Models;
using Mc2.CrudTest.Application.Models.Commands;
using Mc2.CrudTest.Infrastructure.ModelsRepository;
using MediatR;

namespace Mc2.CrudTest.Application.Handlers.Command;

public class DeleteCustomerCommandHandler:IRequestHandler<DeleteCustomerCommand, MetaResponse<bool>>
{
    private ICustomerRepository _customerrepository;

    public DeleteCustomerCommandHandler(ICustomerRepository customerrepository)
    {
        _customerrepository = customerrepository;
    }


    public async Task<MetaResponse<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
      var result=  _customerrepository.Delete(request.Id);

      if (!result)
          return new MetaResponse<bool> {ResponseType = ResponseType.NotFound, Message = " not found"};

      return new MetaResponse<bool> {ResponseType = ResponseType.Success, Result = true};
    }


}