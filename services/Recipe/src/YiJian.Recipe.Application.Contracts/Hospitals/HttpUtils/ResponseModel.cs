using System;
using System.Collections.Generic;

namespace YiJian.Hospitals.Models.HttpUtils
{
    public class ResponseModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Guid UUID { get; set; }

        public DateTime DayOfBirth { get; set; }

        public List<string> Hobbies { get; set; }

        public Dictionary<string, string> Dic { get; set; }

        public List<SubResponseModel> Subs { get; set; }

    }

    public class SubResponseModel
    {
        public int SId { get; set; }
        public string SName { get; set; }

        public List<string> Emails { get; set; }
    }

}
