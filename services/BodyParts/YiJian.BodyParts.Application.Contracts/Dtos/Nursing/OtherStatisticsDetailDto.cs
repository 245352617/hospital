using RabbitMQ.Client;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{

    public class OtherStatisticsItemDto
    {
        /// <summary>
        /// 项目名称
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// 项目结果
        /// </summary>
        public string ItemValue { get; set; }

        public OtherStatisticsItemDto()
        {

        }

        public OtherStatisticsItemDto(string itemName, string itemValue)
        {
            this.ItemName = itemName;
            this.ItemValue = itemValue;
        }
    }
}