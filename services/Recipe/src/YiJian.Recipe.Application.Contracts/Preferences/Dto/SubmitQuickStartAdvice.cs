using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Dto;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.Hospitals.Enums;

namespace YiJian.Preferences.Dto
{
    /// <summary>
    /// 提交的快速开嘱对象
    /// </summary>
    public class SubmitQuickStartAdvice : EntityDto<Guid>
    {
        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary> 
        [Required(ErrorMessage = "系统标识必填，0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        [Required(ErrorMessage = "患者唯一标识必填")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        [StringLength(20, ErrorMessage = "患者ID长度在20个字符内")]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        [StringLength(30, ErrorMessage = "患者名称长度在30个字符内")]
        public string PatientName { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        [Required(ErrorMessage = "申请医生编码必填"), StringLength(20, ErrorMessage = "申请医生编码长度在20个字符内")]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        [Required(ErrorMessage = "申请医生必填"), StringLength(50, ErrorMessage = "申请医生长度在50个字符内")]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary> 
        [StringLength(20, ErrorMessage = "申请科室编码长度在20个字符内")]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary> 
        [StringLength(50, ErrorMessage = "申请科室长度在50个字符内")]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 是否慢性病
        /// </summary> 
        public bool? IsChronicDisease { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>   
        public string Diagnosis { get; set; }

        private string _prescribeTypeCode;

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        [StringLength(20, ErrorMessage = "医嘱类型编码长度在20个字符内")]
        public string PrescribeTypeCode
        {
            get
            {
                if (_prescribeTypeCode.IsNullOrEmpty()) return "PrescribeTemp";
                return _prescribeTypeCode;
            }
            set
            {
                if (value.IsNullOrWhiteSpace())
                {
                    _prescribeTypeCode = "PrescribeTemp";
                }
                else
                {
                    _prescribeTypeCode = value;
                }
            }
        }

        private string _prescribeTypeName;

        /// <summary>
        ///  医嘱类型：临嘱、长嘱、出院带药等
        /// </summary>
        [StringLength(20, ErrorMessage = "医嘱类型长度在20个字符内")]
        public string PrescribeTypeName
        {
            get
            {
                if (_prescribeTypeName.IsNullOrEmpty()) return "临";
                return _prescribeTypeName;
            }
            set
            {
                if (value.IsNullOrWhiteSpace())
                {
                    _prescribeTypeName = "临";
                }
                else
                {
                    _prescribeTypeName = value;
                }
            }
        }

        ///// <summary>
        ///// 药品id （重要,His药品视图过来的invid）
        ///// </summary>
        //[Required(ErrorMessage = "药品id （重要,His药品视图过来的invid）")]
        //public int MedicineId { get; set; }

        /// <summary>
        /// 患者年龄参数，用来检查是否是儿童
        /// </summary>
        public PatientInfoDto PatientInfo { get; set; }

        ///// <summary>
        ///// 是否皮试 false=不需要皮试 true=需要皮试
        ///// </summary> 
        //public bool? IsSkinTest { get; set; } = false;

        ///// <summary>
        ///// 皮试结果:false=阴性 ture=阳性
        ///// </summary> 
        //public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 皮试选择结果 （新增）
        /// </summary>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        ///// <summary>
        ///// 是否医保适应症
        ///// </summary> 
        //public bool? IsAdaptationDisease { get; set; }

        ///// <summary>
        ///// 是否是急救药
        ///// </summary> 
        //public bool? IsFirstAid { get; set; }

        ///// <summary>
        ///// 医保目录编码
        ///// </summary> 
        //[StringLength(20)]
        //public string InsuranceCode { get; set; }

        ///// <summary>
        ///// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        ///// </summary> 
        //public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary> 
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public ERecipePayType PayType { get; set; }

        ///// <summary>
        ///// 抗生素权限
        ///// </summary> 
        //public int AntibioticPermission { get; set; }

        ///// <summary>
        ///// 处方权
        ///// </summary>
        //public int PrescriptionPermission { get; set; }

        ///// <summary>
        ///// 收费类型编码
        ///// </summary> 
        //[StringLength(20)]
        //public string ChargeCode { get; set; }

        ///// <summary>
        ///// 收费类型名称
        ///// </summary> 
        //[StringLength(50)]
        //public string ChargeName { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary> 
        public int LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }

        ///// <summary>
        ///// 儿童价格
        ///// </summary> 
        //public decimal? ChildrenPrice { get; set; }

        ///// <summary>
        ///// 执行科室编码
        ///// </summary> 
        //public string ExecDeptCode { get; set; }

        ///// <summary>
        ///// 执行科室名称
        ///// </summary> 
        //public string ExecDeptName { get; set; }

        ///// <summary>
        ///// 附加类型
        ///// </summary>
        //public EAdditionalItemType AdditionalItemsType { get; set; }

        ///// <summary>
        ///// 药品附加项id
        ///// </summary>
        //public Guid AdditionalItemsId { get; set; }


        /// <summary>
        /// 疫苗接种记录信息，只有当 toxicLevel=7时，传过来
        /// </summary>
        public ImmunizationRecordDto ImmunizationRecord { get; set; }

    }

}
