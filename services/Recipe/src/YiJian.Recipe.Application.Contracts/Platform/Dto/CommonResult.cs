namespace YiJian.Platform.Dto
{
    public class CommonResult<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public bool Success { get { return Code == 0; } }
    }
}
