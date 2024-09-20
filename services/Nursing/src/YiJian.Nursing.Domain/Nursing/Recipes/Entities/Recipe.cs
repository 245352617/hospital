using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.Nursing.Recipes.Entities
{
    /// <summary>
    /// 医嘱单
    /// </summary>
    [Comment("医嘱单")]
    public class Recipe : FullAuditedAggregateRoot<Guid>
    {
        #region 属性

        /// <summary>
        /// HIS医嘱号
        /// </summary>
        [Comment("HIS医嘱号")]
        [StringLength(36)]
        public string HisOrderNo { get; set; }

        /// <summary>
        /// 系统标识: 0=急诊，1=院前
        /// </summary>
        [Comment("系统标识: 0=急诊，1=院前")]
        public EPlatformType PlatformType { get; set; }

        /// <summary>
        /// 医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗
        /// </summary>
        [Comment("医嘱各项分类: 0=药品,1=检查,2=检验,3=诊疗")]
        public ERecipeItemType ItemType { get; set; }

        /// <summary>
        /// 患者入科流水号
        /// </summary>
        [Comment("患者入科流水号")]
        public Guid PIID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        [Comment("患者Id")]
        [StringLength(20)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        [Comment("患者名称")]
        [StringLength(50)]
        public string PatientName { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary>
        [Comment("医嘱编码")]
        [Required, StringLength(20)]
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary>
        [Comment("医嘱名称")]
        [Required, StringLength(200)]
        public string Name { get; set; }

        /// <summary>
        ///  医嘱项目分类编码
        /// </summary>
        [Comment("医嘱项目分类编码")]
        [Required, StringLength(20)]
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary>
        [Comment("医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)")]
        [Required, StringLength(20)]
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary>
        [Comment("是否补录")]
        public bool IsBackTracking { get; set; }

        /// <summary>
        /// 处方号
        /// </summary>
        [Comment("处方号")]
        [StringLength(20)]
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary> 
        [Comment("医嘱号")]
        [Required, StringLength(20)]
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary>
        [Comment("医嘱子号")]
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 医嘱类型编码
        /// </summary>
        [Comment("医嘱类型编码")]
        [Required, StringLength(20)]
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary>
        [Comment("医嘱类型：临嘱、长嘱、出院带药等")]
        [Required, StringLength(20)]
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        [Comment("开嘱时间")]
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 开嘱医生编码
        /// </summary>
        [Comment("开嘱医生编码")]
        [Required, StringLength(20)]
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 开嘱医生名称
        /// </summary>
        [Comment("开嘱医生名称")]
        [Required, StringLength(50)]
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 开嘱科室编码
        /// </summary>
        [Comment("开嘱科室编码")]
        [StringLength(20)]
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 开嘱科室名称
        /// </summary>
        [Comment("开嘱科室名称")]
        [StringLength(50)]
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 管培生编码
        /// </summary>
        [Comment("管培生编码")]
        [StringLength(20)]
        public string TraineeCode { get; set; }

        /// <summary>
        /// 管培生名称
        /// </summary>
        [Comment("管培生名称")]
        [StringLength(50)]
        public string TraineeName { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        [Comment("执行科室编码")]
        [StringLength(20)]
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        [Comment("执行科室名称")]
        [StringLength(50)]
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行
        /// </summary>
        [Comment("医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行")]
        public EDoctorsAdviceStatus Status { get; set; }

        /// <summary>
        /// 医保目录编码
        /// </summary>
        [Comment("医保目录编码")]
        [StringLength(20)]
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary>
        [Comment("医保目录:0=自费,1=甲类,2=乙类,3=其它")]
        public InsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 是否慢性病
        /// </summary>
        [Comment("是否慢性病")]
        public bool? IsChronicDisease { get; set; }


        /// <summary>
        /// 打印次数
        /// </summary> 
        [Comment("打印次数")]
        public int PrintedTimes { get; set; } = 0;

        /// <summary>
        /// 临床诊断
        /// </summary>  
        [Comment("临床诊断")]
        public string Diagnosis { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary>
        [Comment("医嘱说明")]
        [StringLength(500)]
        public string Remarks { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Comment("数量")]
        public int RecieveQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        [Comment("单位")]
        [StringLength(20)]
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        [Comment("开始时间")]
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary> 
        [Comment("结束时间")]
        public DateTime? EndTime { get; set; }
        #endregion 属性

        #region 收费
        /// <summary>
        /// 付费类型编码
        /// </summary>
        [Comment("付费类型编码")]
        [StringLength(20)]
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary>
        [Comment("付费类型: 0=自费,1=医保,2=其它")]
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 收费单位
        /// </summary>
        [Comment("收费单位")]
        [StringLength(20)]
        public string Unit { get; set; }

        /// <summary>
        /// 收费单价
        /// </summary> 
        [Comment("收费单价")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }

        /// <summary>
        /// 总费用
        /// </summary>
        [Comment("总费用")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal Amount { get; set; }

        /// <summary>
        /// 收费类型编码
        /// </summary>
        [Comment("收费类型编码")]
        [StringLength(20)]
        public string ChargeCode { get; set; }

        /// <summary>
        /// 收费类型名称
        /// </summary>
        [Comment("收费类型名称")]
        [StringLength(50)]
        public string ChargeName { get; set; }

        /// <summary>
        /// 支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费
        /// </summary>
        [Comment("支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费")]
        public EPayStatus PayStatus { get; set; } = EPayStatus.NoPayment;

        #endregion 收费

        #region 停嘱

        /// <summary>
        /// 停嘱医生代码
        /// </summary>
        [Comment("停嘱医生代码")]
        [StringLength(20)]
        public string StopDoctorCode { get; set; }

        /// <summary>
        /// 停嘱医生
        /// </summary>
        [Comment("停嘱医生")]
        [StringLength(50)]
        public string StopDoctorName { get; set; }

        /// <summary>
        /// 停嘱生效时间
        /// </summary>
        [Comment("停嘱生效时间")]
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 停嘱操作时间
        /// </summary>
        [Comment("停嘱操作时间")]
        public DateTime? StopOptTime { get; set; }

        #endregion 停嘱

        /// <summary>
        /// 区域编码
        /// </summary>
        [Comment("区域编码")]
        public string AreaCode { get; set; }
    }
}