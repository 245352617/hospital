
namespace YiJian.Documents.Dto
{

    public class ResultUtil<T>
    {

        public int StatusCode
        {
            get;
            set;
        }

        public string EventMsg
        {
            get;
            set;
        }

        public T EventValue
        {
            get;
            set;
        }

    }
}