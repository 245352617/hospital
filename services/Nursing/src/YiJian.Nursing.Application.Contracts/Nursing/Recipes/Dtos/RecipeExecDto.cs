using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace YiJian.Nursing.Recipes
{
    /// <summary>
    /// 描述：医嘱执行单Dto
    /// 创建人： yangkai
    /// 创建时间：2023/3/9 11:02:27
    /// </summary>
    public class RecipeExecDto
    {
        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid RecipeId { get; set; }

        /// <summary>
        /// 执行单Id
        /// </summary>
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 患者PI_Id
        /// </summary>
        public Guid PI_Id { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 医嘱子号
        /// </summary>
        public int RecipeGroupNo { get; set; }

        /// <summary>
        /// 业务类型 0=急诊,1=院前急救
        /// </summary> 
        public int PlatformType { get; set; }

        /// <summary>
        /// 执行单状态
        /// </summary>
        public ExecuteStatusEnum ExecuteStatus { get; set; }

        /// <summary>
        /// 执行单状态
        /// </summary>
        public string ExecuteStatusText { get; set; }

        /// <summary>
        ///  医嘱项目分类编码 
        /// </summary> 
        public string CategoryCode { get; set; }

        /// <summary>
        ///  医嘱项目分类名称
        /// </summary> 
        public string CategoryName { get; set; }

        /// <summary>
        /// 医嘱类型编码
        /// </summary> 
        public string PrescribeTypeCode { get; set; }

        /// <summary>
        /// 医嘱类型： 临嘱、长嘱
        /// </summary> 
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 开立时间(开嘱时间)
        /// </summary>
        public DateTime ApplyTime { get; set; }

        /// <summary>
        /// 医嘱编码
        /// </summary> 
        public string Code { get; set; }

        /// <summary>
        /// 医嘱名称
        /// </summary> 
        public string Name { get; set; }

        /// <summary>
        /// 包装规格（规格
        /// </summary> 
        public string Specification { get; set; }

        /// <summary>
        /// 每次剂量(剂量）
        /// </summary> 
        public decimal? DosageQty { get; set; }

        /// <summary>
        /// 每次剂量单位（单位）
        /// </summary> 
        public string DosageUnit { get; set; }

        /// <summary>
        /// 用法（途径）编码
        /// </summary> 
        public string UsageCode { get; set; }

        /// <summary>
        /// 用法（途径）名称
        /// </summary> 
        public string UsageName { get; set; }

        /// <summary>
        /// 频次码
        /// </summary> 
        public string FrequencyCode { get; set; }

        /// <summary>
        /// 频次名称
        /// </summary> 
        public string FrequencyName { get; set; }

        /// <summary>
        /// 开药天数（天数）
        /// </summary> 
        public int? LongDays { get; set; }

        /// <summary>
        /// 皮试结果 0-阴性 1-阳性
        /// </summary> 
        public bool? SkinTestResult { get; set; }

        /// <summary>
        /// 是否已经打印瓶贴
        /// </summary>
        public bool IsPrint { get; set; }

        /// <summary>
        /// 皮试结果
        /// </summary>
        public string SkinTestResultText
        {
            get
            {
                return SkinTestResult.HasValue ? (SkinTestResult.Value ? "阳性" : "阴性") : string.Empty;
            }
        }

        /// <summary>
        /// 单价(金额) 
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 开嘱医生编码
        /// </summary> 
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 开嘱医生
        /// </summary> 
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 核对护士
        /// </summary>
        public string CheckNurseCode { get; set; }

        /// <summary>
        /// 核对护士名称
        /// </summary>
        public string CheckNurseName { get; set; }

        /// <summary>
        /// 核对时间
        /// </summary>
        public DateTime? CheckTime { get; set; }

        /// <summary>
        /// 二次核对护士
        /// </summary>
        public string TwoCheckNurseCode { get; set; }

        /// <summary>
        /// 二次核对护士名称
        /// </summary>
        public string TwoCheckNurseName { get; set; }

        /// <summary>
        /// 二次核对时间
        /// </summary>
        public DateTime? TwoCheckTime { get; set; }

        /// <summary>
        /// 执行护士
        /// </summary>
        public string ExcuteNurseCode { get; set; }

        /// <summary>
        /// 执行护士名称
        /// </summary>
        public string ExcuteNurseName { get; set; }

        /// <summary>
        /// 护士执行时间
        /// </summary>
        public DateTime? ExcuteNurseTime { get; set; }

        /// <summary>
        /// 总剂量
        /// </summary>
        public decimal Dosage { get; set; }

        /// <summary>
        /// 备用量
        /// </summary>
        public decimal ReserveDosage { get; set; }

        /// <summary>
        /// 执行量
        /// </summary>
        public decimal? TotalDosage { get; set; }

        /// <summary>
        /// 余量
        /// </summary>
        public decimal? TotalRemainDosage { get; set; }

        /// <summary>
        /// 弃液量
        /// </summary>
        public decimal? TotalDiscardDosage { get; set; }

        /// <summary>
        /// 计量单位
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// 是否废弃
        /// </summary>
        public bool? IsDiscard { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public string Order { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int RecieveQty { get; set; }

        /// <summary>
        /// 单位
        /// </summary>
        public string RecieveUnit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 就诊区域编码
        /// </summary>
        public string AreaCode { get; set; }
    }

    /// <summary>
    /// 医嘱分页列表返回Dto
    /// </summary>
    public class ResultDataDto
    {
        /// <summary>
        /// 数据
        /// </summary>
        public List<RecipeExecDto> RecipeDtos { get; set; }

        /// <summary>
        /// 统计数据
        /// </summary>
        public Dictionary<string, int> StatisticsData { get; set; }

        /// <summary>
        /// 总条数
        /// </summary>
        public int Count { get; set; }
    }

    /// <summary>
    /// 去重筛选
    /// </summary>
    public class RecipeExecOrderDto : IEqualityComparer<RecipeExecOrderDto>
    {
        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 比较是否相同
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public bool Equals(RecipeExecOrderDto x, RecipeExecOrderDto y)
        {
            return x.RecipeNo == y.RecipeNo && x.PlanExcuteTime == y.PlanExcuteTime;
        }

        /// <summary>
        /// 获取HashCode
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int GetHashCode([DisallowNull] RecipeExecOrderDto obj)
        {
            return obj.RecipeNo.GetHashCode() ^ obj.PlanExcuteTime.GetHashCode();
        }
    }
}
