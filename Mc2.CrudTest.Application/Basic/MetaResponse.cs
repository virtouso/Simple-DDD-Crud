using Mc2.CrudTest.Application.Basic.Enums;

namespace Mc2.CrudTest.Application.Basic;

public class MetaResponse<T>
{
    public bool IsError => ResponseType != (ResponseType.Success);
    public string Message { get; set; } = "";
    public ResponseType ResponseType { get; set; }
    public T? Result { get; set; } = default;
 
}