using System;
using System.ComponentModel.DataAnnotations;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 新增病人手术Dto
    /// </summary>
    public class CreatePatientOperationDto
    {
        /// <summary>
        /// 住院号（一次住院期间不会变）
        /// </summary>
        public string InHosId { get; set; }

        /// <summary>
        /// 病人流水号，病人在一次住院期间不会变
        /// 因系统中绝大多数业务都是使用该字段，故添加了该字段
        /// </summary>
        [Required(ErrorMessage = "PI_ID为必填参数")]
        public string PI_ID { get; set; }

        /// <summary>
        /// 手术名称
        /// </summary>
        [Required(ErrorMessage = "手述名称为必填参数")]
        public string OperationName { get; set; }

        /// <summary>
        /// 麻醉方式
        /// </summary>
        public string AnesthesiaMode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary>
        public string ApplicationDepartment { get; set; }

        /// <summary>
        /// 手术等级
        /// </summary>
        public string OperationLevel { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        /// 2021-10-13 因汕大业务需求，该字段改为非必填
        // [Required(ErrorMessage = "申请时间为必填参数")]
        public DateTime? ApplyTime { get; set; }

        /// <summary>
        /// 手术时间
        /// </summary>
        [Required(ErrorMessage = "手术时间不能为空")]
        public DateTime OperationTime { get; set; }

        /// <summary>
        /// 术中输血
        /// </summary>
        public decimal? OperationInputPlasma { get; set; }

        /// <summary>
        /// 术中出血
        /// </summary>
        public decimal? OperationOutputPlasma { get; set; }

        /// <summary>
        /// 术中输液
        /// </summary>
        public decimal? OperationInput { get; set; }

        /// <summary>
        /// 术中平衡
        /// </summary>
        public decimal? OperationBalance { get; set; }

        /// <summary>
        /// 术中尿量
        /// </summary>
        public decimal? Vol { get; set; }

        /// <summary>
        /// 是否显示
        /// </summary>
        public bool IsShow { get; set; }
    }

    /// <summary>
    /// 修改病人Dto
    /// </summary>
    public class UpdatePatientOperationDto : CreatePatientOperationDto
    {
        /// <summary>
        /// 主键
        /// </summary>
        public Guid id { get; set; }
    }
    
    /// <summary>
    /// 病人手术详情
    /// </summary>
    public class PatientOperationDto : UpdatePatientOperationDto
    {
        
    }

    public class RefreshPatientOperationDto
    {
        /// <summary>
        /// 病人流水号，病人在一次住院期间不会变
        /// 因系统中绝大多数业务都是使用该字段，故添加了该字段
        /// </summary>
        [Required(ErrorMessage = "PI_ID为必填参数")]
        public string PI_ID { get; set; }
        /// <summary>
        /// 患者id
        /// </summary>
        [Required(ErrorMessage = "PatientId为必填参数")]
        public string PatientId { get; set; }
    }
}
