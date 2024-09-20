namespace YiJian.ECIS.Call
{
    using System;

    [Serializable]
    public class DepartmentData 
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        public bool IsActived { get; set; }
    }
}
