using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:员工
    /// </summary>
    public class IcuStaffDto : EntityDto<Guid>
    {
        /// <summary>
        /// 员工代码
        /// </summary>
        /// <example></example>
        public string StaffCode { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        /// <example></example>
        public string StaffName { get; set; }

        /// <summary>
        /// 拼音
        /// </summary>
        /// <example></example>
        public string PinYin { get; set; }

        /// <summary>
        /// 用户代码
        /// </summary>
        /// <example></example>
        public string UserCode { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        /// <example></example>
        public string Gender { get; set; }

        /// <summary>
        /// 出生日期
        /// </summary>
        /// <example></example>
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 职务
        /// </summary>
        /// <example></example>
        public string Job { get; set; }

        /// <summary>
        /// 职称：医生、护士
        /// </summary>
        /// <example></example>
        public string JobTitle { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        public string DeptCode { get; set; }

        ///// <summary>
        ///// 签名图片
        ///// </summary>
        ///// <example></example>
        //public byte[] SignImage { get; set; }

        /// <summary>
        /// 签名类型
        /// </summary>
        /// <example></example>
        public string SignType { get; set; }

        /// <summary>
        /// Ukey号码
        /// </summary>
        /// <example></example>
        public string UKeySN { get; set; }

        /// <summary>
        /// 一级签名
        /// </summary>
        /// <example></example>
        public string Sign1 { get; set; }

        /// <summary>
        /// 二级签名
        /// </summary>
        /// <example></example>
        public string Sign2 { get; set; }

        /// <summary>
        /// 三级签名
        /// </summary>
        /// <example></example>
        public string Sign3 { get; set; }

        /// <summary>
        /// 护理记录审核权限（Y,N）
        /// </summary>
        public string IsAudit { get; set; }

        /// <summary>
        /// 有效标注
        /// </summary>
        /// <example></example>
        public string ValidState { get; set; }
    }
}
