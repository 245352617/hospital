namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class ResponseResult<T>
    {
        public HttpStatusCodeEnum Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public dynamic Extra { get; set; }
    }
}
