using Mc2.CrudTest.Application.Basic;
using MediatR;

namespace Mc2.CrudTest.Application.Models.Commands;

public class DeleteCustomerCommand :  IRequest<MetaResponse<bool>>
{
   public  Int64 Id { get; set; } 
}