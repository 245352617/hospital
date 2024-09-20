using Microsoft.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Entities;
using YiJian.Health.Report.Enums;

namespace YiJian.Health.Report.NursingDocuments.Entities
{
    /// <summary>
    /// 指尖血糖 mmol/L
    /// </summary>
    [Comment("指尖血糖 mmol/L")]
    public class Mmol : Entity<Guid>
    {
        private Mmol()
        {

        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id"></param>
        /// <param name="mealTimeType"></param>
        /// <param name="value"></param>
        /// <param name="nursingRecordId"></param>
        public Mmol(Guid id, EMealTimeType mealTimeType, string value, Guid nursingRecordId)
        {
            Id = id;
            MealTimeType = mealTimeType;
            Value = value;
            NursingRecordId = nursingRecordId;
        }

        /// <summary>
        /// 餐前餐后(0=餐前，1=餐后)
        /// </summary>
        [Comment("餐前餐后(0=餐前，1=餐后)")]
        public EMealTimeType MealTimeType { get; set; }

        /// <summary>
        /// 数值
        /// </summary>
        [Comment("数值")]
        public string Value { get; set; }

        /// <summary>
        /// 护理记录Id
        /// </summary>
        [Comment("护理记录Id")]
        public Guid NursingRecordId { get; set; }

        public void Update(EMealTimeType mealTimeType, string value)
        {
            MealTimeType = mealTimeType;
            Value = value;
        }

    }
}
