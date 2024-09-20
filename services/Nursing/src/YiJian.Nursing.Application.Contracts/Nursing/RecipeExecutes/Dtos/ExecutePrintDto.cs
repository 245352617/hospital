using System;
using System.Collections.Generic;

namespace YiJian.Nursing.RecipeExecutes
{
    /// <summary>
    /// 描述：医嘱执行单
    /// 创建人： yangkai
    /// 创建时间：2023/3/30 10:41:46
    /// </summary>
    public class ExecutePrintDto
    {
        /// <summary>
        /// 姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string Age { get; set; }

        /// <summary>
        /// 打印时间
        /// </summary>
        public string PrintTime { get; set; }

        /// <summary>
        /// 科室
        /// </summary>
        public string DeptName { get; set; }

        /// <summary>
        /// 床号
        /// </summary>
        public string Bed { get; set; }

        /// <summary>
        /// 就诊号
        /// </summary>
        public string VisitNo { get; set; }

        /// <summary>
        /// 医嘱类型
        /// </summary>
        public string PrescribeTypeName { get; set; }

        /// <summary>
        /// 医嘱执行列表
        /// </summary>
        public List<ExecuteDetail> ExecuteDetails { get; set; }
    }

    /// <summary>
    /// 医嘱执行列表
    /// </summary>
    public class ExecuteDetail
    {
        /// <summary>
        /// 执行单id
        /// </summary>
        public Guid RecipeExecId { get; set; }

        /// <summary>
        /// 开嘱日期
        /// </summary>
        public string ApplyDate { get; set; }

        /// <summary>
        /// 医嘱号
        /// </summary>
        public string RecipeNo { get; set; }

        /// <summary>
        /// 开嘱时间
        /// </summary>
        public string ApplyTime { get; set; }

        /// <summary>
        /// 医嘱项目
        /// </summary>
        public string RecipeName { get; set; }

        /// <summary>
        /// 剂量
        /// </summary>
        public string DosageQty { get; set; }

        /// <summary>
        /// 用法
        /// </summary>
        public string UsageName { get; set; }

        /// <summary>
        /// 开嘱医生
        /// </summary>
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 计划执行时间
        /// </summary>
        public DateTime PlanExcuteTime { get; set; }

        /// <summary>
        /// 执行时间
        /// </summary>
        public string ExecuteTime { get; set; }

        /// <summary>
        /// 执行人
        /// </summary>
        public string ExecuteName { get; set; }

        /// <summary>
        /// 核对时间
        /// </summary>
        public string TwoCheckTime { get; set; }

        /// <summary>
        /// 核对人
        /// </summary>
        public string TwoCheckNurseName { get; set; }

        /// <summary>
        /// 核对时间
        /// </summary>
        public string CheckTime { get; set; }

        /// <summary>
        /// 核对人
        /// </summary>
        public string CheckName { get; set; }

        /// <summary>
        /// 途径编码
        /// </summary>
        public string UsageCode { get; set; }
    }
}
