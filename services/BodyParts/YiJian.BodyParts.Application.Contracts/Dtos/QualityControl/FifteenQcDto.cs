using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using YiJian.BodyParts.Domain.Shared.Const;
using JetBrains.Annotations;

namespace YiJian.BodyParts.Dtos
{

    #region 质控统计Dto ICU质量控制指标Dto

    //t.PI_ID ,it.OrderText,it.ValidState



    /// <summary>
    /// 存放医嘱基本信息
    /// </summary>
    public class OrderDtoInit
    {

        /// <summary>
        /// 入科iD
        /// </summary>
        public String PI_ID { get; set; }


        /// <summary>
        /// 医嘱信息
        /// </summary>
        public String OrderText { get; set; }



        /// <summary>
        /// 是否有效
        /// </summary>
        public int ValidState { get; set; }

    }



    #region Icu基类


    /// <summary>
    /// Icu基类
    /// </summary>

    public class ICU
    {
        /// <summary>
        /// 患者名称
        /// </summary>
        public virtual string PatientName { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string InHosId { get; set; }



        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }



        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }



        /// <summary>
        /// 出生日期
        /// </summary>
        public DateTime? Birthday { get; set; }


    }


    #endregion


    #region 质控统计Dto


    /// <summary>
    /// 质控统计Dto
    /// </summary>

    public class QualityDto : ICU
    {


        /// <summary>
        /// 患者ID
        /// </summary>

        public string VisitNum { get; set; }

        /// <summary>
        /// Vte 评分
        /// </summary>

        public string Vte { get; set; }

        /// <summary>
        /// Vte 评分等级
        /// </summary>

        public string VteGlevel { get; set; }

        /// <summary>
        /// 转入标准
        /// </summary>

        public string InStandard { get; set; }

        /// <summary>
        /// 转出标准
        /// </summary>

        public string OutStandard { get; set; }


        public string ArchiveId { get; set; }

        /// <summary>
        /// 质控导出休克
        /// </summary>
        /// <param name="icuPatientSepsisDetailsDtos"></param>
        /// <param name="ItemName"></param>
        /// <returns></returns>

        public string IsChearTubtu(List<IcuPatientSepsisDetailsDto> icuPatientSepsisDetailsDtos, string ItemName)
        {
            if(SepticShock == "否")
            {
                return "/";
            }
            else
            {
                var Data = icuPatientSepsisDetailsDtos.Where(it => it.ItemName.Contains(ItemName)).FirstOrDefault();

                if (Data == null)
                {
                    return "未完成";
                }

                return Data.Status;
            }
        }


        /// <summary>
        ///  出科转归(1：出院，2：转出，3：死亡）
        /// </summary>
        public string OutTurnover { get; set; }


        /// <summary>
        /// 出科状态
        /// </summary>
        public int ? OutState  { get; set; }


        /// <summary>
        /// 转出科室 
        /// </summary>
        public string OutDeptName { get; set; }


        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }



        /// <summary>
        /// 床号(采集器编号)
        /// </summary>
        public string BedNo { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public string? InDeptTime { get; set; }


        /// <summary>
        /// 出科时间
        /// </summary>
        public string? OutDeptTime { get; set; }

        /// <summary>
        /// 在科天数
        /// </summary>
        public int Days { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }

        /// <summary>
        /// 出科诊断 
        /// </summary>
        public string Outdiagnosis { get; set; }

        /// <summary>
        /// 入科来源 
        /// </summary>
        public string InSource { get; set; }



        /// <summary>
        /// 来源科室名称
        /// </summary>
        public string InDeptName { get; set; }


        /// <summary>
        ///  入科计划（0：非计划转入，1：计划转入）
        /// </summary>
        public string InPlan { get; set; }

        /// <summary>
        /// 重返 
        /// </summary>
        public string InReturn { get; set; }

        /// <summary>
        /// APACHEII  APACHEII
        /// </summary>
        public string ApacheII { get; set; }

        /// <summary>
        ///  预计病死率
        /// </summary>
        public string ExpectedDeadRate { get; set; }


        /// <summary>
        /// 抗菌药物治疗
        /// </summary>

        public int AntibioticsRate { get; set; } = 0;



        /// <summary>
        /// 药物名称
        /// </summary>
        public string Antibacterial_drug_name { get; set; }


        /// <summary>
        ///  治疗模式
        /// </summary>
        public string USE_PURPOSE { get; set; }



        /// <summary>
        /// 治疗方式
        /// </summary>
        public string INSPECTION { get; set; }


        /// <summary>
        ///  SOFA 评分
        /// </summary>
        public string Sofa { get; set; }


        /// <summary>
        /// DVT预防
        /// </summary>
        public string DVT { get; set; }

        /// <summary>
        /// 拔管次数(包括非正常拔管)
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        /// 非计划拔管次数(意外脱管)
        /// </summary>
        public int NoNumber { get; set; }

        /// <summary>
        /// 48H再插管次数
        /// </summary>
        public int SbHnoNumber { get; set; }

        /// <summary>
        /// 机械通气天数
        /// </summary>
        public int VentilationDays { get; set; }

        /// <summary>
        /// 有创天数
        /// </summary>
        public int InvasiveDays { get; set; }

        /// <summary>
        /// 无创天数
        /// </summary>
        public int NoInvasiveDays { get; set; }

        /// <summary>
        /// 血管内导管留置天数
        /// </summary>
        public int IndwellingDays { get; set; }


        /// <summary>
        /// 尿管留置天数
        /// </summary>
        public int UrinaryDays { get; set; }

        /// <summary>
        /// 动态
        /// </summary>

        public List<Dictionary<string, object>> DictionaryData { get; set; } = new List<Dictionary<string, object>>();


        /// <summary>
        /// 感染性休克
        /// </summary>
        public string SepticShock { get; set; }


        /// <summary>
        /// 测量乳酸
        /// </summary>

        public string RrEp { get; set; }

        /// <summary>
        /// 血培养
        /// </summary>

        public string RrEper { get; set; }


        /// <summary>
        /// 广谱抗菌药物
        /// </summary>

        public string RrEpewr { get; set; }


        /// <summary>
        /// 液体复苏
        /// </summary>

        public string RrEpeFr { get; set; }

        /// <summary>
        /// 测量CVP和ScvO2
        /// </summary>

        public string RrEpeF6r { get; set; }

        /// <summary>
        /// 复测乳酸
        /// </summary>

        public string RrEpeFs6r { get; set; }

        /// <summary>
        /// 升压药治疗
        /// </summary>

        public string RrEpeFx6r { get; set; }
    }

    #endregion



    /// <summary>
    /// ICU质量控制指标等级表Dto
    /// </summary>
    public class IcuQualityControlDto : ICU
    {


        public string VisitNum { get; set; }

        /// <summary>
        /// 是否第一次
        /// </summary>

        public bool IsFirst { get; set; } = true;

        /// <summary>
        /// 签名
        /// </summary>
        public string Signature { get; set; }


        /// <summary>
        /// 患者id
        /// </summary>
        public string InHosNun { get; set; }


        /// <summary>
        /// 入科计划
        /// </summary>
        public IcuInformation IcuInformation { get; set; } = new IcuInformation();



        /// <summary>
        /// 出科信息
        /// </summary>
        public OutIcuInformation OutIcuInformation { get; set; } = new OutIcuInformation();



        /// <summary>
        /// DVT预防Dto
        /// </summary>
        public DvtPrevention DvtPrevention { get; set; } = new DvtPrevention();


        /// <summary>
        /// 脓毒症dto
        /// </summary>
        public IcuPatientSepsisDto IcuPatientSepsisDto { get; set; } = new IcuPatientSepsisDto();


        /// <summary>
        /// 抗菌药物
        /// </summary>

        public EstimatedDto EstimatedDto { get; set; } = new EstimatedDto();

        /// <summary>
        /// 管道相关
        /// </summary>
        public PipelineDto PipelineDto { get; set; } = new PipelineDto();

        /// <summary>
        /// 其他质量数据
        /// </summary>
        public OtherQualityDto OtherQualityDto { get; set; } = new OtherQualityDto();

    }



    #region 入科信息

    public class IcuInformation
    {
        /// <summary>
        /// 入Iuc时间 取最近的一次
        /// </summary>
        public string IcuDay { get; set; }


        /// <summary>
        /// 人icu的总次数
        /// </summary>
        public int IcuNumBer { get; set; }



        /// <summary> 
        ///重返（0：否，1：24小时重返，2：48小时重返）
        /// </summary>
        public int? InReturn { get; set; } =0;


        /// <summary>
        /// 是否急诊
        /// </summary>

        public bool Istreatment { get; set; } = false;


        /// <summary>
        /// 来源科室名称
        /// </summary>
        public string InDeptName { get; set; }



        /// <summary>
        /// 入科状况分类
        /// </summary>
        public int? ClassType { get; set; }


        ///  是否非计划转入（0：非计划转入，1：计划转入）
        /// </summary>
        public int? InPlan { get; set; }

        /// <summary>
        /// 转入ICU诊断
        /// </summary>
        public string Indiagnosis { get; set; } = string.Empty;




        /// <summary>
        /// 24小时APACHEII 评分
        /// </summary>
        public string ApacheII { get; set; } = string.Empty;


        /// <summary>
        /// Gcs 评分
        /// </summary>
        public int  Gcs { get; set; } =0;



        /// <summary>
        /// 预计病死率
        /// </summary>
        public string ExpectedDeadRate { get; set; }


        /// <summary>
        /// 24小时SOFA评分
        /// </summary>
        public string Sofa { get; set; }



        /// <summary>
        ///  是否符合转入ICU标准
        /// </summary>
        public IcuMeets IcuMeets { get; set; } = new IcuMeets();


    }




    /// <summary>
    /// ICU标准
    /// </summary>
    public class DictSourceIcuInformation
    {
        /// <summary>
        /// 转出类型
        /// </summary>
        public string ModuleName { get; set; }



        /// <summary>
        /// 描述
        /// </summary>
        public string ParaName { get; set; }



        /// <summary>
        /// 是否选中
        /// </summary>

        public bool IsSelectValue { get; set; } = false;


        /// <summary>
        /// 是否多选
        /// </summary>
        public bool ParaValue { get; set; }
    }



    /// <summary>
    /// 是否符合转入ICU标准
    /// </summary>
    public class IcuMeets
    {

        /// <summary>
        /// 是否符合
        /// </summary>

        public bool IsFu { get; set; } = false;


        /// <summary>
        /// 是否符合转入ICU标准
        /// </summary>
        public List<DictSourceIcuInformation> DictSourceIcuInformation = new List<DictSourceIcuInformation>();

    }



    /// <summary>
    /// 是否符合转出ICU标准
    /// </summary>
    public class OutMeets
    {
        /// <summary>
        /// 是否符合
        /// </summary>

        public bool IsFu { get; set; } = false;



        /// <summary>
        /// 是否符合转出ICU标准
        /// </summary>
        public List<DictSourceIcuInformation> DictSourceIcuInformation = new List<DictSourceIcuInformation>();
    }


    #endregion

    #region 出科信息 

    public class OutIcuInformation
    {

        /// <summary>
        /// 出ICU日期
        /// </summary>
        public string OutIcuData { get; set; }


        /// <summary>
        /// 住ICU天数
        /// </summary>
        public int OutIcuNubBer { get; set; }



        /// <summary>
        /// 转出科室 
        /// </summary>
        public string OutDeptName { get; set; }



        /// <summary>
        ///  出科转归(1：出院，2：转出，3：死亡）
        /// </summary>
        public int? OutTurnover { get; set; }



        /// <summary>
        /// 是否符合转出ICU标准
        /// </summary>
        public OutMeets OutMeets { get; set; } = new OutMeets();



        /// <summary>
        /// 出ICU诊断
        /// </summary>

        public string Outdiagnosis { get; set; } = string.Empty;


        /// <summary>
        /// 出科状态
        /// </summary>
        public int? OutState { get; set; }

    }

    #endregion

    #region 脓毒症dto

    /// <summary>
    /// 脓毒症dto
    /// </summary>
    public class IcuPatientSepsisDto
    {
        /// <summary>
        /// 是否诊断脓毒症休克
        /// </summary>
        public bool IsConfirmed { get; set; } = false;



        /// <summary>
        /// 3小时bundle完成 是否
        /// </summary>

        public bool IsSanbundle { get; set; } = false;

        /// <summary>
        /// 3小时bundle
        /// </summary>
        public List<IcuPatientSepsisDetailsDto> Sanbundle { get; set; } = new List<IcuPatientSepsisDetailsDto>();



        /// <summary>
        /// 6小时bundle完成 是否
        /// </summary>

        public bool IsLiuanbundle { get; set; } = false;

        /// <summary>
        /// 6小时bundle
        /// </summary>
        public List<IcuPatientSepsisDetailsDto> Liuanbundle { get; set; } = new List<IcuPatientSepsisDetailsDto>();


    }


    /// <summary>
    /// 脓毒症子表
    /// </summary>
    public class IcuPatientSepsisDetailsDto
    {

        /// <summary>
        /// 名称
        /// </summary>
        public string ItemName { get; set; } = string.Empty;


        /// <summary>
        /// 是否确定确认类型，2：人工确认，0：未确认，1：取消确认
        /// </summary>
        public int? Type { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public string Status { get; set; } = string.Empty;
    }

    #endregion

    #region 抗菌药物

    /// <summary>
    /// 抗菌药物
    /// </summary>
    public class EstimatedDto
    {
        /// <summary>
        /// 是否以治疗为目的使用抗菌药物
        /// </summary>
        public bool Estimated { get; set; } = false;



        /// <summary>
        /// 抗菌药物前送检病原学检验标本
        /// </summary>
        public bool EstimatedCase { get; set; } = false;



        /// <summary>
        /// 送检病原学标本
        /// </summary>
        public Fatalityrate FatalityrateDto { get; set; } = new Fatalityrate();

    }


    /// <summary>
    /// 送检病原学标本
    /// </summary>
    public class Fatalityrate
    {

        /// <summary>
        /// 微生物培养
        /// </summary>
        public bool Microbial { get; set; } = false;


        /// <summary>
        /// PCT
        /// </summary>
        public bool Pct { get; set; } = false;


        /// <summary>
        /// IL-6等感染指标的血清学检验
        /// </summary>
        public bool IL6 { get; set; } = false;
    }

    #endregion

    #region DVT预防


    /// <summary>
    /// DVT预防Dto
    /// </summary>
    public class DvtPrevention
    {

        /// <summary>
        /// VTE高危评分
        /// </summary>
        public string VteMark { get; set; }


        /// <summary>
        /// 是否行深静脉血栓
        /// </summary>
        public bool DveMark { get; set; } = false;


        /// <summary>
        /// 药物预防
        /// </summary>
        public DvtPreventionMedicineItem DvtPreventionMedicine { get; set; } = new DvtPreventionMedicineItem();



        /// <summary>
        /// 机械预防
        /// </summary>
        public DvtPreventionJxItem DvtPreventionJxItem { get; set; } = new DvtPreventionJxItem();

    }


    /// <summary>
    /// 药物预防
    /// </summary>

    public class DvtPreventionMedicineItem
    {
        /// <summary>
        /// 药物预防
        /// </summary>
        public bool Drugbial { get; set; } = false;


        /// <summary>
        /// 低分子量肝素钙
        /// </summary>
        public bool Oral { get; set; } = false;



        /// <summary>
        /// 依诺肝素纳/克赛
        /// </summary>
        public bool Yral { get; set; } = false;



        /// <summary>
        /// 那屈肝素钙/速碧林
        /// </summary>
        public bool Nral {  get; set; } = false;



        /// <summary>
        /// 利伐沙班/拜瑞妥
        /// </summary>

        public bool Lral {  get; set; } = false;



        /// <summary>
        /// 华法林钠
        /// </summary>
        public bool Low { get; set; } = false;
    }


    /// <summary>
    /// 机械预防
    /// </summary>
    public class DvtPreventionJxItem
    {
        /// <summary>
        /// 机械预防
        /// </summary>
        public bool JxDrugbial { get; set; } = false;

        /// <summary>
        /// 肢体加压泵
        /// </summary>
        public bool Limb { get; set; } = false;


        /// <summary>
        /// 梯度压力弹力袜
        /// </summary>
        public bool Socks { get; set; } = false;

        /// <summary>
        /// 下腔静脉滤器
        /// </summary>
        public bool Xxlvb { get; set; } = false;
    }






    #endregion

    #region 管道相关
    /// <summary>
    /// 管道相关
    /// </summary>
    public class PipelineDto
    {
        /// <summary>
        /// 拔除气管插管次数
        /// </summary>
        public int Tracheal { get; set; } = 0;

        /// <summary>
        /// 非计划气管拔管次数（注：包括患者意外拔管及放弃治疗拔管）
        /// </summary>
        public int NoTracheal { get; set; } = 0;

        /// <summary>
        /// 气管插管拔管后48h内再插次数（注：不包括非计划气管插管拔管后再插管）
        /// </summary>
        public int SbNoTracheal { get; set; } = 0;

        /// <summary>
        /// 机械通气总天数
        /// </summary>
        public int JxDays { get; set; } = 0;

        /// <summary>
        /// 有创通气天数
        /// </summary>
        public int CyDays { get; set; } = 0;

        /// <summary>
        /// 无创通气
        /// </summary>
        public int WcDays { get; set; } = 0;

        /// <summary>
        ///  VAP发生次数
        /// </summary>
        public int Vap { get; set; } = 0;


        /// <summary>
        ///  血管内导管留置总天数
        /// </summary>
        public int XgdDays { get; set; } = 0;



        /// <summary>
        ///   CRBSI发生次数
        /// </summary>
        public int CrbsiDays { get; set; } = 0;



        /// <summary>
        ///   导尿管留置总天数
        /// </summary>
        public int NgDays { get; set; } = 0;


        /// <summary>
        /// CAUTI发生次数
        /// </summary>
        public int CautiDays { get; set; } = 0;

    }


    #endregion

    #region 其他质量数据

    public class OtherQualityDto
    {
        /// <summary>
        /// 气管插管术
        /// </summary>
        public int TrachealDays { get; set; }

        /// <summary>
        /// 气管切开术
        /// </summary>
        public bool KtrachealCheark { get; set; } = false;


        /// <summary>
        /// 经皮气管切开术
        /// </summary>
        public bool ZtrachealCheark { get; set; } = false;


        /// <summary>
        /// 连续性血液净化（CRRT/CBP）
        /// </summary>
        public bool CrrtCheark { get; set; } = false;


        /// <summary>
        /// PICCO
        /// </summary>
        public bool PiccoCheark { get; set; } = false;

        /// <summary>
        /// IABP
        /// </summary>
        public bool IABPCheark { get; set; } = false;


        /// <summary>
        /// 纤维支气管镜次
        /// </summary>
        public int QtrachealCheark { get; set; }


        /// <summary>
        /// 血浆置换次
        /// </summary>
        public int PlasmaNunBer { get; set; }


        /// <summary>
        /// 血液灌流/血浆吸附次
        /// </summary>
        public int PlasGxmaNunBer { get; set; }

        /// <summary>
        /// ECMO
        /// </summary>
        public bool EcmoCheark { get; set; } = false;

        /// <summary>
        /// Icp
        /// </summary>
        public bool IcpCheark { get; set; } = false;


        /// <summary>
        /// IAP次
        /// </summary>
        public bool IAPCheark { get; set; } = false;

        /// <summary>
        /// 亚低温治疗次
        /// </summary>
        public bool YadiwCheark { get; set; } = false;
    }
    #endregion

    #endregion



    /// <summary>
    /// 设备使用时长
    /// </summary>

    public class VentilatorDurationDto : ICU
    {

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }




        /// <summary>
        /// 入科诊断
        /// </summary>
        public string Indiagnosis { get; set; }


        /// <summary>
        /// 入科时间
        /// </summary>
        public string? InDeptTime { get; set; }


        /// <summary>
        /// 出科时间
        /// </summary>
        public string? OutDeptTime { get; set; }

        /// <summary>
        /// 泵入类药物执行时间
        /// </summary>
        public int? PumpHours { get; set; }


     
        /// <summary>
        /// 动态
        /// </summary>

        public List<Dictionary<string, object>> DictionaryData { get; set; } = new List<Dictionary<string, object>>();
    }



    /// <summary>
    /// 体征查询Dto
    /// </summary>
    public class SignQueryMainDto
    {



        /// <summary>
        /// 出生日期
        /// </summary>

        public DateTime? Birthday { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary>
        public Guid Id { get; set; }



        /// <summary>
        /// 患者id(通过业务构造的流水号，每个患者每次入科号码唯一)
        /// </summary>
        [StringLength(20)]
        [Required]
        public string PI_ID { get; set; }

        /// <summary>
        /// 档案号
        /// </summary>
        [StringLength(20)]
        [CanBeNull]
        public string ArchiveId { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string VisitNum { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string InHosId { get; set; }


        /// <summary>
        /// 性别
        /// </summary>
        public string Gender { get; set; }



        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 入科时间
        /// </summary>
        public string InDeptTime { get; set; }


        /// <summary>
        /// 床号
        /// </summary>
        public string BedNum { get; set; }

    }


    /// <summary>
    /// 体征查询子表
    /// </summary>
    public class SignQuerySubDto
    {

        /// <summary>
        /// 执行时间
        /// </summary>
        public DateTime NurseTime{ get; set; }



        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParName{ get; set; }



        /// <summary>
        /// 项目结果
        /// </summary>
        public string ParValue { get; set; }


        /// <summary>
        /// 项目单位
        /// </summary>
        public string UnitName { get; set; }


        /// <summary>
        /// 记录时间
        /// </summary>
        public  DateTime?  RecordTime { get; set; }


        /// <summary>
        /// 操作人
        /// </summary>
        public string NurseName { get; set; }
    }


    /// <summary>
    /// 15项质控统计
    /// </summary>
    public class FifteenQcDto
    {
        /// <summary>
        /// 时间列
        /// </summary>
        [Description("时间列")]
        public string[] Times { get; set; }

        /// <summary>
        /// 15项质控详细数据
        /// </summary>
        [Description("15项质控详细数据")]
        public List<DetailData> detailDatas { get; set; }
    }

    /// <summary>
    /// 15项质控详细数据
    /// </summary>
    public class DetailData
    {
        public string Code { get; set; }

        public string[] Rate { get; set; }

        public List<Children> childrens { get; set; }
    }

    public class Children
    {
        public string[] Sum { get; set; }
    }
}
