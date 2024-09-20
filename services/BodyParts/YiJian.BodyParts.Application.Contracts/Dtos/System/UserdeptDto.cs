using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class UserdeptDto
    {
        public string DeptCode { get; set; }
        public string DeptName { get; set; }

        /// <summary>
        /// 医生D、护士N
        /// </summary>
        public string JobTitle { get; set; }
    }
}
