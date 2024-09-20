namespace HisApiMockService.Models;

public class BaseResponse<T>
{
    public BaseResponse(T data)
    {
        Code = 0;
        Msg = "ok";
        Data = data;
    }
     
    public int Code { get; set; }

    public string Msg { get; set; }

    public T Data { get; set; }
     
}
