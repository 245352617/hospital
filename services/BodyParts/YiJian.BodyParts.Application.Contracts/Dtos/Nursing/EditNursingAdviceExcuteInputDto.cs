using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class EditNursingAdviceExcuteInputDto:EntityDto<Guid>
    {
        /// <summary>
        /// 修改人工号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 修改人姓名
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string NursingRemark { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? NurseTime { get; set; }
    }
}
