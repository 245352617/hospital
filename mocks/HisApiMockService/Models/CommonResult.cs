namespace HisApiMockService.Models;

public class CommonResult<T>
{
    public int Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }
}
