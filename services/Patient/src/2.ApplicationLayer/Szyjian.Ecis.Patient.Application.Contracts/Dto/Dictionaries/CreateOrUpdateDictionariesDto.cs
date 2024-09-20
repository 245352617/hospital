using Szyjian.Ecis.Patient.Domain.Shared;

namespace Szyjian.Ecis.Patient.Application.Contracts
{
    public class CreateOrUpdateDictionariesDto
    {
        /// <summary>
        /// 主键id
        /// </summary>
        public int Id
        {
            get;
            set;
        } = -1;
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
        public DictionariesType DictionariesTypeCode { get; set; }

        /// <summary>
        /// 使用状态
        /// </summary>
        public bool Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 扩展字段 （床位大小 小：1 大：2）
        /// </summary>
        public string ExtendField1 { get; set; }
    }
}