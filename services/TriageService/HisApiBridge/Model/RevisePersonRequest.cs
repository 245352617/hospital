using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 修改主索引人员/修改患者基本信息接口入参
    /// </summary>
    public class RevisePersonRequest
    {
        /// <summary>
        /// 患者 ID
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        /// <value></value>
        public string visitNo { get; set; }

        /// <summary>
        /// 姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 性别（1男2女）
        /// </summary>
        /// <example>1</example>
        public string gender { get; set; }

        /// <summary>
        /// 性别（女性、男性）
        /// </summary>
        /// <example>女性</example>
        public string genderName { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// <example>1973-10-12</example>
        public string birthDate { get; set; }

        /// <summary>
        /// 联系人标识类别
        /// 01：手机号码
        /// </summary>
        /// <example>01</example>
        public string contactType { get; set; } = "01";

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 联系人证件号码
        /// </summary>
        public string contactNo { get; set; }

        /// <summary>
        /// 联系人手机号
        /// </summary>
        /// <example>18381847987</example>
        public string content { get; set; }

        /// <summary>
        /// 地址信息
        /// </summary>
        /// <example>广东-深圳-坪山区-马峦街道碧玲新榕路桂花巷5号</example>
        public string addrDetail { get; set; }

        /// <summary>
        /// 证件号码
        /// </summary>
        public string idCode { get; set; }

        /// <summary>
        /// 证件类型
        /// </summary>
        public string idType { get; set; }

        /// <summary>
        /// 证件类型名称
        /// </summary>
        public string idTypeName { get; set; }
    }
}
