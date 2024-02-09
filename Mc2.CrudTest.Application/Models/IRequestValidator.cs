namespace Mc2.CrudTest.Application.Models;

public interface IRequestValidator<T,S>
{
    S Validate(T request);
}