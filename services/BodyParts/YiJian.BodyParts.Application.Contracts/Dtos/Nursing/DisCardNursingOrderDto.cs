using System;
using YiJian.BodyParts.Dtos;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Nursing
{
    public class DisCardNursingOrderDto : CreateNursingOrderDto
    {
        /// <summary>
        /// 医嘱名称
        /// </summary>
        public string OrderText { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }
    }
}
