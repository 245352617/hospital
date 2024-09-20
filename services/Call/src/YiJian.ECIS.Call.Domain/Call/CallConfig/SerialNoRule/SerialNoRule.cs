using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp;
using Volo.Abp.Domain.Entities.Auditing;

namespace YiJian.ECIS.Call.Domain.CallConfig
{
    /// <summary>
    /// 【排队号规则】领域实体
    /// </summary>
    public class SerialNoRule : AuditedAggregateRoot<int>
    {
        /// <summary>
        /// 科室id
        /// </summary>
        [Required(ErrorMessage = "科室id必填")]
        public virtual Guid DepartmentId { get; private set; }

        /// <summary>
        /// 开头字母
        /// </summary>
        [Required(ErrorMessage = "开头字母必填")]
        [StringLength(8, ErrorMessage = "开头字母长度不能大于8位")]
        public virtual string Prefix { get; private set; }

        /// <summary>
        /// 流水号位数
        /// </summary>
        [Required(ErrorMessage = "位数必填")]
        public virtual ushort SerialLength { get; private set; }

        /// <summary>
        /// 当前流水号
        /// </summary>
        public int CurrentNo { get; private set; }

        /// <summary>
        /// 危急患者当前流水号
        /// </summary>
        public int CriticalCurrentNo { get; private set; }

        /// <summary>
        /// 当前流水号编码日期
        /// 当日期过期时需要重置流水号
        /// </summary>
        public DateTime SerialDateTime { get; set; }

        protected SerialNoRule()
        {
        }

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="departmentId">科室id</param>
        /// <param name="prefix">开头字母</param>
        /// <param name="serialLength">位数</param>
        public SerialNoRule([NotNull] Guid departmentId, string prefix, ushort serialLength)
        {
            this.DepartmentId = Check.NotNull(departmentId, nameof(DepartmentId));
            this.Prefix = prefix;
            this.SerialLength = serialLength;
        }

        /// <summary>
        /// 修改规则
        /// </summary>
        /// <param name="departmentId">科室id</param>
        /// <param name="prefix">开头字母</param>
        /// <param name="serialLength">位数</param>
        public SerialNoRule Update([NotNull] Guid departmentId, string prefix, ushort serialLength)
        {
            this.DepartmentId = Check.NotNull(departmentId, nameof(DepartmentId));
            this.Prefix = prefix;
            this.SerialLength = serialLength;

            return this;
        }

        /// <summary>
        /// 重置流水号
        /// </summary>
        /// <returns></returns>
        public SerialNoRule ResetSerialNo(int initSerialNo)
        {
            this.CurrentNo = initSerialNo;
            this.CriticalCurrentNo = 0;
            this.SerialDateTime = DateTime.Now;

            return this;
        }

        /// <summary>
        /// 下一个排队号（叫号）
        /// </summary>
        public SerialNoRule GoNext()
        {
            this.CurrentNo++;
            this.SerialDateTime = DateTime.Now;

            return this;
        }


        /// <summary>
        /// 下一个排队号（叫号）
        /// </summary>
        public SerialNoRule GoCriticalNext()
        {
            this.CriticalCurrentNo++;
            this.SerialDateTime = DateTime.Now;

            return this;
        }
    }
}
