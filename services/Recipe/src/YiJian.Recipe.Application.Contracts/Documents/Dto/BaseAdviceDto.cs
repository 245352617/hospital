using System;
using Volo.Abp.Application.Dtos;
using YiJian.DoctorsAdvices.Enums;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Extensions;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// 医嘱基础信息
    /// </summary>
    public class BaseAdviceDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者唯一标识
        /// </summary> 
        public Guid PIID { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }


        /// <summary>
        ///  医嘱项目分类编码
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        /// 医嘱项目分类 (药物，检查，检验，治疗，护理，膳食，麻醉，手术，会诊，耗材，其他，嘱托)
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 是否补录
        /// </summary> 
        public bool IsBackTracking { get; set; }

        /// <summary>
        /// 处方号
        /// </summary> 
        public string PrescriptionNo { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>  
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号（同组下参数修改）
        /// </summary> 
        public int RecipeGroupNo { get; set; } = 1;

        /// <summary>
        /// 开嘱时间
        /// </summary> 
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 开嘱时间 格式化
        /// </summary> 
        public string ApplyTimeToString { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary> 
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室
        /// </summary> 
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 医嘱状态:0=未提交,1=已提交,2=已确认,3=已作废,4=已停止,6=已驳回,7=已执行,18=已缴费,19=已退费
        /// </summary> 
        public ERecipeStatus Status { get; set; }

        /// <summary>
        /// 付费类型编码
        /// </summary> 
        public string PayTypeCode { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public ERecipePayType PayType { get; set; }

        /// <summary>
        /// 付费类型: 0=自费,1=医保,2=其它
        /// </summary> 
        public string PayTypeName
        {
            get { return PayType.GetDescription(); }
        }

        /// <summary>
        /// 单位
        /// </summary> 
        public string Unit { get; set; }

        /// <summary>
        /// 单价
        /// </summary>  
        public decimal Price { get; set; }

        /// <summary>
        /// 总费用
        /// </summary> 
        public decimal Amount { get; set; }

        /// <summary>
        /// 医保目录编码
        /// </summary> 
        public string InsuranceCode { get; set; }

        /// <summary>
        /// 医保目录:0=自费,1=甲类,2=乙类,3=其它
        /// </summary> 
        public EInsuranceCatalog InsuranceType { get; set; }

        /// <summary>
        /// 是否慢性病
        /// </summary> 
        public bool? IsChronicDisease { get; set; }

        /// <summary>
        /// HIS医嘱号
        /// </summary> 
        public string HisOrderNo { get; set; }

        /// <summary>
        /// 临床诊断
        /// </summary>   
        public string Diagnosis { get; set; }

        /// <summary>
        /// 医嘱说明
        /// </summary> 
        public string Remarks { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型：临嘱、长嘱、出院带药等
        /// </summary> 
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 领量(数量)
        /// </summary> 
        public decimal RecieveQty { get; set; }

        /// <summary>
        /// 领量单位
        /// </summary> 
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string ExecutorCode { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string ExecutorName { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime? ExecTime { get; set; }

        /// <summary>
        /// 是否打印过
        /// </summary> 
        public bool IsRecipePrinted { get; set; }

        #region HIS反馈回来的记录

        /// <summary>
        /// 医嘱类型 处方：CF   非处方:YJ
        /// </summary>
        public string MedType { get; set; }

        /// <summary>
        /// 渠道识别号  4.5.3医嘱信息回传（his提供、需对接集成平台） chargeDetailNo projectItemNo
        /// </summary>
        public string ChannelNumber { get; set; }

        /// <summary>
        /// His识别号 对应his处方识别（C）、医技序号（Y）  可用于二维码展示等
        /// </summary>
        public string HisNumber { get; set; }

        /// <summary>
        /// HIS申请单号 处方：处方号码  医技：申请单id（检验、检查返回）
        /// </summary>
        public string ChannelNo { get; set; }

        /// <summary>
        /// 病历号 患者主索引id、用于条形码展示
        /// </summary>
        public string MedicalNo { get; set; }

        /// <summary>
        /// 支付二维码  深圳市龙岗中心医院微信公众
        /// </summary>
        public string Lgzxyy_payurl { get; set; }

        /// <summary>
        /// 支付二维码 深圳市龙岗健康在线支付二维码
        /// </summary>
        public string Lgjkzx_payurl { get; set; }

        /// <summary>
        /// 性质 用于申请单性质显示
        /// </summary>
        public string MedNature { get; set; }

        /// <summary>
        /// 费别 用于申请单费别显示
        /// </summary>
        public string MedFee { get; set; }
        #endregion

        /// <summary>
        /// 云签
        /// </summary>
        public string StampBase { get; set; }
        /// <summary>
        /// 是否是加收价格 true=是加收价格， false=不是加收价格
        /// </summary>
        public bool IsAdditionalPrice { get; set; }

        /// <summary>
        /// 指引名称
        /// </summary>
        public string GuideName { get; set; }

        /// <summary>
        /// 检查单单名标题
        /// </summary>
        public string ExamTitle { get; set; }

        /// <summary>
        /// 预约地点
        /// </summary>
        public string ReservationPlace { get; set; }

        /// <summary>
        /// 预约时间
        /// </summary>
        public string ReservationTime { get; set; }

        /// <summary>
        /// 指引大类
        /// </summary>
        public string GuideCatelogName { get; set; }

        /// <summary>
        /// 处方返回时间
        /// </summary>
        public string ResultTime { get; set; }

        /// <summary>
        /// 是否已打
        /// </summary>
        public string PrintStr { get; set; }

        /// <summary>
        /// 提交序列号，每次提交只生成一个序列号,0=没有提交序号
        /// </summary>
        public int CommitSerialNo { get; set; }

        /// <summary>
        /// 来源
        /// </summary>
        public int SourceType { get; set; }
    }
}