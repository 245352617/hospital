using System;
using Newtonsoft.Json;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    public class IcuNursingAdviceContentDto : EntityDto<Guid>
    {
        #region 基础属性
        /// <summary>
        /// 护嘱内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 护嘱类别编号
        /// </summary>
        public Guid CategoryId { get; set; }
        /// <summary>
        /// 护嘱类别名称
        /// </summary>
        public string CategoryName { get; set; }
        /// <summary>
        /// 频次编号
        /// </summary>
        public Guid FrequencyId { get; set; }
        /// <summary>
        /// 频次名称
        /// </summary>
        public string FrequencyName { get; set; }

        /// <summary>
        /// 排序序列号
        /// </summary>
        public long? SortNum { get; set; }
        #endregion

        #region 扩展属性

        /// <summary>
        /// 使用患者流水号号查询护嘱内容时，该字段表示此护嘱是否已经开立，或是否已经停止
        /// 未使用患者患者id查询时，此字段始终返回空。
        /// </summary>
        public string Status { get; set; } = string.Empty;

        #endregion

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Include });
        }

    }
}
