namespace Szyjian.Ecis.Patient.Domain.Shared
{
    public class RespUtil
    {
        public static ResponseResult<T> Ok<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.Ok, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> Error<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.Error, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> UnAuthorized<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.UnAuthorized, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> Forbidden<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.Forbidden, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> NotFound<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.NotFound, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> InternalError<T>(HttpStatusCodeEnum code = HttpStatusCodeEnum.InternalError, string msg = null, T data = default(T), string extra = null)
        {
            return CustomResp(code, msg, data, extra);
        }

        public static ResponseResult<T> CustomResp<T>(HttpStatusCodeEnum code, string msg, T data = default(T), string extra = null)
        {
            if (string.IsNullOrWhiteSpace(msg))
            {
                msg = code.GetDescription();
            }

            return new ResponseResult<T>
            {
                Code = code,
                Msg = msg,
                Data = data,
                Extra = extra
            };
        }
    }
}
