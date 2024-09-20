using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class DictionariesDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public Guid Id
        {
            get;
            set;
        }
        /// <summary>
        /// 字典编码
        /// </summary>
        public string DictionariesCode { get; set; }

        /// <summary>
        /// 字典名称
        /// </summary>
        public string DictionariesName { get; set; }

        /// <summary>
        /// 字典类型编码
        /// </summary>
        public string DictionariesTypeCode { get; set; }
        /// <summary>
        /// 字典类型名称
        /// </summary>
        public string DictionariesTypeName { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }
    }

}
