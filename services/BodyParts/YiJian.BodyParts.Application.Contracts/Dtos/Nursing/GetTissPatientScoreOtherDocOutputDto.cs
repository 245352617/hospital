using System;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class GetTissPatientScoreOtherDocOutputDto
    {
        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatName { get; set; }

        /// <summary>
        /// 评估人，管床护士
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 班次 A P N
        /// </summary>
        public string ScheduleCode { get; set; }

        /// <summary>
        /// 团队所属组-组长
        /// </summary>
        public string TeamGroup_Master { get; set; }

        /// <summary>
        /// 团队所属组-质控班
        /// </summary>
        public string TeamGroup_QC { get; set; }

        /// <summary>
        /// 团队所属组-办公班
        /// </summary>
        public string TeamGroup_Office { get; set; }

        /// <summary>
        /// 团队所属组-组办
        /// </summary>
        public string TeamGroup_Group { get; set; }

        /// <summary>
        /// 仪器 1 勾选
        /// </summary>
        public string Instrument { get; set; }

        /// <summary>
        /// 药品 1 勾选
        /// </summary>
        public string Drug { get; set; }

        /// <summary>
        /// 监管新护士
        /// </summary>
        public string SupervisionOfNurse { get; set; }

        /// <summary>
        /// 带跟班护士
        /// </summary>
        public string FollowNurse { get; set; }

        /// <summary>
        /// 负责三甲事务
        /// </summary>
        public string TopThreeTransition { get; set; }

        /// <summary>
        /// 带实习同学
        /// </summary>
        public string InternshipStudent { get; set; }

        /// <summary>
        /// 外出抢救
        /// </summary>
        public string Rescue { get; set; }

        /// <summary>
        /// 院内状态-院内学习
        /// </summary>
        public string InHosStatus_Study { get; set; }

        /// <summary>
        /// 院内状态-院内开会 3：院内检查
        /// </summary>
        public string InHosStatus_Meeting { get; set; }

        /// <summary>
        /// 院内状态-院内检查
        /// </summary>
        public string InHosStatus_Insp { get; set; }

        /// <summary>
        /// 外出状态-外出学习
        /// </summary>
        public string OutHosStatus_Study { get; set; }

        /// <summary>
        /// 外出状态-外出开会 3：外出检查
        /// </summary>
        public string OutHosStatus_Meeting { get; set; }

        /// <summary>
        /// 外出状态-外出检查
        /// </summary>
        public string OutHosStatus_Insp { get; set; }

        /// <summary>
        /// 科研班
        /// </summary>
        public string ScienceClass { get; set; }

        /// <summary>
        /// 当班负责人
        /// </summary>
        public string OnDutyPerson { get; set; }
        
        /// <summary>
        /// 新收病人
        /// </summary>
        public string IsNewPatBed { get; set; }

        /// <summary>
        /// 新收1位
        /// </summary>
        public string NewPatBed_One { get; set; }

        /// <summary>
        /// 新收2位
        /// </summary>
        public string NewPatBed_Two { get; set; }

        /// <summary>
        /// 新收3位
        /// </summary>
        public string NewPatBed_Three { get; set; }

        /// <summary>
        /// 总分
        /// </summary>
        public string TotalScore { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime { get; set; }
    }
}