using System;
using Volo.Abp.Application.Dtos;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 指尖血糖 mmol/L
    /// </summary> 
    public class MmolDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 餐前餐后(0=餐前，1=餐后)
        /// </summary> 
        public EMealTimeType MealTimeType { get; set; }

        /// <summary>
        /// 数值
        /// </summary> 
        public string Value { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        public Guid NursingRecordId { get; set; }

    }


    /// <summary>
    /// 指尖血糖 mmol/L
    /// </summary> 
    public class MmolBaseDto
    {
        /// <summary>
        /// 餐前餐后(0=餐前，1=餐后)
        /// </summary> 
        public EMealTimeType MealTimeType { get; set; }

        /// <summary>
        /// 数值
        /// </summary> 
        public string Value { get; set; }

    }

}
