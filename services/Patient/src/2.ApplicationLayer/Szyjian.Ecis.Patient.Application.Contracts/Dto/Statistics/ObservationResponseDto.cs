using System;
using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class ObservationResponseDto
    {
        public int Id { get; set; }

        public int VisitStatus { get; set; }

        [Excel("状态", 500)]
        public string VisitStatusName { get; set; }

        [Excel("门诊号码", 490)]
        public string VisitNo { get; set; }

        [Excel("姓名", 480)]
        public string PatientName { get; set; }

        [Excel("性别", 470)]
        public string SexName { get; set; }

        [Excel("年龄", 460)]
        public string Age { get; set; }

        [Excel("入留操作人", 450)]
        public string InObservation { get; set; }

        [Excel("出留操作人", 440)]
        public string OutObservation { get; set; }

        public DateTime InObservationTime { get; set; }

        [Excel("入留时间", 430)]
        public string InObservationTime1 { get; set; }

        public DateTime? FirstRecipeTime { get; set; }

        [Excel("首次医嘱", 420)]
        public string FirstRecipeTime1 { get; set; }

        public DateTime? OutObservationTime { get; set; }

        [Excel("出留时间", 410)]
        public string OutObservationTime1 { get; set; }

        public double ObservationRetentionTime { get; set; }

        [Excel("时长", 400)]
        public string ObservationRetentionTime1 { get; set; }

        [Excel("小时", 390)]
        public string ObservationRetentionTime2 { get; set; }

        public string TriageLevel { get; set; }

        [Excel("分级", 380)]
        public string TriageLevelName { get; set; }

        public string BedHeadSticker { get; set; }

        [Excel("病情", 370)]
        public string BedHeadStickerName { get; set; }

        public string ToArea { get; set; }

        [Excel("流转去向", 360)]
        public string ToAreaName { get; set; }

        [Excel("病人去向", 350)]
        public string TransferReason { get; set; }

        [Excel("住院科室", 340)]
        public string InpatientDepartmentName { get; set; }

        [Excel("留观区总费用", 330)]
        public decimal ObservationAmount { get; set; }

        [Excel("性质", 320)]
        public string ChargeTypeName { get; set; }

        public bool IsOpenGreenChannl { get; set; }

        [Excel("绿色通道", 310)]
        public string IsOpenGreenChannlName { get; set; }

        [Excel("诊断内容", 300)]
        public string DiagnoseNames { get; set; }

        [Excel("病历内容", 290)]
        public string EmrContent { get; set; }
    }
}
