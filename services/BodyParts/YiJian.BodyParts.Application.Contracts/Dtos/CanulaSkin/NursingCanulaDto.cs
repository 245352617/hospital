using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:导管护理信息
    /// </summary>
    public class NursingCanulaDto
    {
        /// <summary>
        /// 导管记录Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 插管天数
        /// </summary>
        /// <example></example>
        public int Days { get; set; }

        /// <summary>
        /// 持续天数
        /// </summary>
        /// <example></example>
        // public int ContinDays { get; set; }

        /// <summary>
        /// 导管分类
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 导管分类名称
        /// </summary>
        /// <example></example>
        public string ModuleName { get; set; }

        /// <summary>
        /// 模块显示名称
        /// </summary>
        public string DisplayName { get; set; }


        /// <summary>
        /// 管道名称
        /// </summary>
        /// <example></example>
        public string CanulaName { get; set; }

        /// <summary>
        /// 插管部位
        /// </summary>
        /// <example></example>
        public string CanulaPart { get; set; }


        /// <summary>
        /// 插管时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 插管次数
        /// </summary>
        /// <example></example>
        public int? CanulaNumber { get; set; }

        /// <summary>
        /// 插管地点
        /// </summary>
        public string CanulaPosition { get; set; }

        /// <summary>
        /// 置入方式
        /// </summary>
        /// <example></example>
        public string CanulaWay { get; set; }

        /// <summary>
        /// 置管长度
        /// </summary>
        /// <example></example>
        public string CanulaLength { get; set; }

        /// <summary>
        /// 置管人代码
        /// </summary>
        /// <example></example>
        public string DoctorId { get; set; }

        /// <summary>
        /// 置管人名称
        /// </summary>
        /// <example></example>
        public string DoctorName { get; set; }

        /// <summary>
        /// 护士Id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public DateTime? NurseTime { get; set; }

        /// <summary>
        /// 拔管原因
        /// </summary>
        /// <example></example>
        public string DrawReason { get; set; }

        /// <summary>
        /// 拔管时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 使用标志：（Y在用，N已拔管）
        /// </summary>
        /// <example></example>
        public string UseFlag { get; set; }

        /// <summary>
        /// 人体图部位编号
        /// </summary>
        /// <example></example>
        public string PartNumber { get; set; }

        /// <summary>
        /// 风险级别 默认空，1低危 2中危 3高危
        /// </summary>
        public string RiskLevel { get; set; }

        /// <summary>
        /// 动态列表
        /// </summary>
        public List<CanulaItemDto> CanulaItemDto { get; set; }
    }

    /// <summary>
    /// 导管/皮肤返回参数
    /// </summary>
    public class CanulaItemDto
    {
        /// <summary>
        /// 动态参数Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 项目代码
        /// </summary>
        public string ParaCode { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        public string ParaName { get; set; }

        /// <summary>
        /// 项目是否静态显示
        /// </summary>
        public string DictFlag { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }

        /// <summary>
        /// 导管分类：管道属性、管道观察/皮肤分类：皮肤属性、皮肤观察
        /// </summary>
        public string GroupName { get; set; }

        /// <summary>
        /// 文本类型(文本框、时间框、单选框、复选框)
        /// </summary>
        public string Style { get; set; }

        /// <summary>
        /// 文本框默认值
        /// </summary>
        public string DataSource { get; set; }

        /// <summary>
        /// 下拉框字典值
        /// </summary>
        public List<Dicts> Dicts { get; set; }
    }

    /// <summary>
    /// 下拉框字典值
    /// </summary>
    public class Dicts
    {
        /// <summary>
        /// Id
        /// </summary>
        public Guid DictCanulaPartId { get; set; }

        /// <summary>
        /// 参数代码
        /// </summary>
        public string DictId { get; set; }

        /// <summary>
        /// 参数名称
        /// </summary>
        public string DictName { get; set; }

        /// <summary>
        /// 字典代码
        /// </summary>
        public string DictCode { get; set; }

        /// <summary>
        /// 字典值
        /// </summary>
        public string DictValue { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int SortNum { get; set; }
    }
}
