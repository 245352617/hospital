using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 表:病人观察项表
    /// </summary>
    public class IcuPatientParaDto : EntityDto<Guid>
    {


        /// <summary>
        /// 患者id
        /// </summary>
        /// <example></example>
        public string PI_ID { get; set; }

        /// <summary>
        /// 模块代码
        /// </summary>
        /// <example></example>
        public string ModuleCode { get; set; }

        /// <summary>
        /// 参数编号
        /// </summary>
        /// <example></example>
        public string ParaCode { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 排列顺序
        /// </summary>
        /// <example></example>
        public int? SortNum { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        /// <example></example>
        // 2021-04-01     ding  因调整病人参数的排序时前端没有传该参数导至接口报错，该参数在调整排序中不会用到，故斩将该参数改为非必填，建议修改排序接口入参与其它接口入参分开
        public string DeptCode { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        /// <example></example>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 停用时间
        /// </summary>
        /// <example></example>
        public DateTime? StopTime { get; set; }

        /// <summary>
        /// 记录时间
        /// </summary>
        /// <example></example>
        public DateTime? RecordTime { get; set; }

        /// <summary>
        /// 操作护士工号
        /// </summary>
        /// <example></example>
        public string NurseCode { get; set; }

        /// <summary>
        /// 操作护士名称
        /// </summary>
        /// <example></example>
        public string NurseName { get; set; }

        /// <summary>
        /// 停止护士工号
        /// </summary>
        /// <example></example>
        public string StopNurseCode { get; set; }

        /// <summary>
        /// 停止护士名称
        /// </summary>
        /// <example></example>
        public string StopNurseName { get; set; }

        /// <summary>
        /// 添加次数
        /// </summary>
        /// <example></example>
        public int? AddTimes { get; set; }

        /// <summary>
        /// 添加来源
        /// </summary>
        /// <example></example>
        public string AddSource { get; set; }

        /// <summary>
        /// 插管编号
        /// </summary>
        /// <example></example>
        public string CanulaId { get; set; }

        /// <summary>
        /// 有效状态（1-有效，0-无效）
        /// </summary>
        /// <example></example>
        public int ValidState { get; set; }
    }
}
