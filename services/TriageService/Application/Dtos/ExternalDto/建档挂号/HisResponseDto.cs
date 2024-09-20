using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.TriageService.Application.Dtos
{
    public class HisResponseDto
    {
        public int code { get; set; }

        public string msg { get; set; }
    }

    public class HisResponseDto<T>
    {
        public int code { get; set; }

        public string msg { get; set; }

        public T data { get; set; }
    }
}
