using System;

namespace YiJian.Hospitals.Models.HttpUtils
{
    public class RequestModel
    {
        public int Id { get; set; }

        public Guid UUID { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public DateTime DayOfBirth { get; set; }

        public SubRequestModel Sub { get; set; }

    }

    public class SubRequestModel
    {
        public int SId { get; set; }

        public string SName { get; set; }

    }
}
