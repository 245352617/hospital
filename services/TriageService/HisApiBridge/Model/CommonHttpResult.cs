using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class CommonHttpResult
    {
        public int Code { get; set; }

        public string Msg { get; set; }
    }

    public class CommonHttpResult<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }
    }

    public class CommonHttpResult<TCode, TData>
    {
        public TCode Code { get; set; }

        public string Msg { get; set; }

        public TData Data { get; set; }
    }
}
