using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:文书护理时间
    /// </summary>
    public class DocumentNurseTimeDto : EntityDto<Guid>
    {
        /// <summary>
        /// 文书Id
        /// </summary>
        public Guid? DocumentId { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        /// <example></example>
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 护理时间(去除秒)
        /// </summary>
        /// <example></example>
        public DateTime NurseTime2
        {
            get
            {
                return Convert.ToDateTime(NurseTime.ToString("yyyy-MM-dd HH:mm"));
            }
        }

        /// <summary>
        /// 护士工号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 签名表外键
        /// </summary>
        /// <example></example>
        public Guid? SignatureCode { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 添加来源（EM：ECMO，BP：血液净化，PC：PICCO）
        /// </summary>
        public string AddSource { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
