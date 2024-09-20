using System;
using System.Collections.Generic;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{

    public class SavePatientScoreOtherInputDto
    {
        /// <summary>
        /// 病人评分id
        /// </summary>
        public Guid Pid { get; set; }

        /// <summary>
        /// 班次 A P N
        /// </summary>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 仪器 1 勾选
        /// </summary>
        public string Instrument { get; set; }

        /// <summary>
        /// 药品 1 勾选
        /// </summary>
        public string Drug { get; set; }

        /// <summary>
        /// 带跟班护士
        /// </summary>
        public string FollowNurse { get; set; }

        /// <summary>
        /// 带实习同学
        /// </summary>
        public string InternshipStudent { get; set; }

        /// <summary>
        /// 院内状态 1：院内学习 2：院内开会 3：院内检查
        /// </summary>
        public string InHosStatus { get; set; }

        /// <summary>
        /// 科研班
        /// </summary>
        public string ScienceClass { get; set; }

        /// <summary>
        /// 当班负责人
        /// </summary>
        public string OnDutyPerson { get; set; }

        /// <summary>
        /// 团队所属组  1：组长 2：质控班 3：办公班 4：组办
        /// </summary>
        public string TeamGroup { get; set; }

        /// <summary>
        /// 监管新护士
        /// </summary>
        public string SupervisionOfNurse { get; set; }

        /// <summary>
        /// 负责三甲事务
        /// </summary>
        public string TopThreeTransition { get; set; }

        /// <summary>
        /// 外出抢救
        /// </summary>
        public string Rescue { get; set; }

        /// <summary>
        /// 外出状态 1：外出学习 2：外出开会 3：外出检查
        /// </summary>
        public string OutHosStatus { get; set; }

        /// <summary>
        /// 新收病人数量
        /// </summary>
        public string NewPatNum { get; set; }

        /// <summary>
        /// 新收病人对应床
        /// </summary>
        public string NewPatBed { get; set; }

        /// <summary>
        /// 记录人
        /// </summary>
        public string NurseCode { get; set; }
    }
}