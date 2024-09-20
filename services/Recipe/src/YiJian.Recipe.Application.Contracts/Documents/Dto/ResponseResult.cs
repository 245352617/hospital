using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Documents.Dto
{
    public class ResponseResult<T>
    {
        public EHttpStatusCodeEnum Code
        {
            get;
            set;
        }

        public string Msg
        {
            get;
            set;
        }

        public T Data
        {
            get;
            set;
        }

        public dynamic Extra
        {
            get;
            set;
        }
    }
}
