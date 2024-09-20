using FreeSql.DataAnnotations;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace Szyjian.Ecis.Patient
{
    /// <summary>
    /// 绿通记录
    /// </summary>
    [Table(Name = "Pat_GreenChannlRecord")]
    public class GreenChannlRecord : Entity<Guid>
    {
        /// <summary>
        /// 设置主键
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public GreenChannlRecord SetId(Guid id)
        {
            Id = id;
            return this;
        }

        /// <summary>
        /// 院前分诊患者建档表主键Id
        /// </summary>
        [Description("院前分诊患者建档表主键Id")]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 自增Id
        /// </summary>
        public int AR_ID { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Description("开始时间")]
        public DateTime BeginTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        [Description("结束时间")]
        public DateTime? EndTime { get; set; }

        /// <summary>
        /// 开通病症
        /// </summary>
        [Description("开通病症")]
        [StringLength(20)]
        public string DicName { get; set; }

        /// <summary>
        /// 开通病症code
        /// </summary>
        [Description("开通病症编码")]
        [StringLength(20)]
        public string DicCode { get; set; }

        /// <summary>
        /// 是否开通绿通
        /// </summary>
        [Description("是否开通绿通")]
        public bool IsOpen { get; set; } = true;

        /// <summary>
        /// 操作时间
        /// </summary>
        [Description("操作时间")]
        public DateTime? OperationTime { get; set; }


        /// <summary>
        /// 操作人
        /// </summary>
        [Description("操作人")]
        public string OperationUser { get; set; }
    }
}