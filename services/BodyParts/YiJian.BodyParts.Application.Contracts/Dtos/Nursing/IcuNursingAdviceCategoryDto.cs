using System;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 护嘱类别模板
    /// </summary>
    public class IcuNursingAdviceCategoryDto : EntityDto<Guid>
    {
        #region 基础属性
        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 护嘱类别名称
        /// </summary>
        [CanBeNull]
        public string Name { get; set; }

        /// <summary>
        /// 排序序号（规则：排序位置乘以10，例如：排序第一位，1 * 10 = 10）
        /// </summary>
        public long? SortNum { get; set; }
        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

    }
}
