using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 皮肤护理记录
    /// </summary>
    public class SkinDto
    {
        /// <summary>
        /// 皮肤护理记录Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 皮肤记录Id
        /// </summary>
        public Guid SkinId { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        public string NurseTime { get; set; }

        /// <summary>
        /// 护理时间(去除秒)
        /// </summary>
        /// <example></example>
        public DateTime NurseTime2
        {
            get
            {
                return Convert.ToDateTime(Convert.ToDateTime(NurseTime).ToString("yyyy-MM-dd HH:mm"));
            }
        }

        /// <summary>
        /// 护士Id
        /// </summary>
        public string NurseId { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 压疮面积
        /// </summary>
        public string PressArea { get; set; }

        /// <summary>
        /// 动态列表
        /// </summary>
        public List<CanulaItemDto> CanulaItemDto { get; set; }

        /// <summary>
        /// 皮肤护理记录
        /// </summary>
        public string CanulaRecord { get; set; } = string.Empty;

        /// <summary>
        /// 签名图片
        /// </summary>
        public string Signature { get; set; } = string.Empty;
    }
}
