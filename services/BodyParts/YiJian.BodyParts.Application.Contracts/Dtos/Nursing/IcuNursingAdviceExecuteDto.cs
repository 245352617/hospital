using System;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuNursingAdviceExecuteDto : EntityDto<Guid>
    {
        #region 基础属性
        /// <summary>
        /// 护嘱
        /// </summary>
        public string Advice { get; set; }

        /// <summary>
        /// 计划开始时间
        /// </summary>
        /// <value></value>
        public DateTime PlanStartTime { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime? NurseTime { get; set; }

        /// <summary>
        /// 护嘱执行人工号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护嘱执行人姓名
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 执行状态（0-待执行，1-已执行，2-未执行）
        /// </summary>
        /// <value></value>
        public long ExecuteStatus { get; set; }

        /// <summary>
        /// 护嘱执行说明
        /// </summary>
        public string NursingRemark { get; set; }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

    }
}
