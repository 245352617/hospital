using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 患者护嘱开立记录
    /// </summary>
    public class IcuNursingAdviceDto : EntityDto<Guid>
    {
        #region 基础属性
        /// <summary>
        /// 患者患者id
        /// </summary>
        public string PI_ID { get; set; }
        /// <summary>
        /// 护嘱开立人工号
        /// </summary>
        public string StartNurseCode { get; set; }
        /// <summary>
        /// 护嘱开立人姓名
        /// </summary>
        public string StartNurseName { get; set; }
        /// <summary>
        /// 护嘱计划开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }
        /// <summary>
        /// 护嘱类别标识
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 护嘱类别名称
        /// </summary>
        public string Category { get; set; }
        /// <summary>
        /// 护嘱内容标识
        /// </summary>
        public Guid ContentId { get; set; }
        /// <summary>
        /// 护嘱内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 护嘱频次标识
        /// </summary>
        public Guid FrequencyId { get; set; }
        /// <summary>
        /// 执行频率
        /// </summary>
        public string Frequency { get; set; }
        #endregion
        
        #region 护嘱停止相关属性
        /// <summary>
        /// 护嘱停止标志（Y-是，N-否）
        /// </summary>
        public string StopFlag { get; set; }
        /// <summary>
        /// 护嘱停止人工号
        /// </summary>
        public string StopNurseCode { get; set; }
        /// <summary>
        /// 护嘱停止人姓名
        /// </summary>
        public string StopNurseName { get; set; }
        /// <summary>
        /// 护嘱的停止时间
        /// </summary>
        public DateTime? StopTime { get; set; }
        #endregion

        #region 护嘱执行相关属性

        /// <summary>
        /// 护嘱执行计划清单
        /// </summary>
        public List<IcuNursingAdviceExecuteDto> AdviceExecutes { get; set; } = new List<IcuNursingAdviceExecuteDto>();

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

    }
}