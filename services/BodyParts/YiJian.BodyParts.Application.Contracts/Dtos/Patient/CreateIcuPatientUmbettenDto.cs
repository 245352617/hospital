using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 换床管理Dto
    /// </summary>
    public class CreateIcuPatientUmbettenDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者id(通过业务构造的流水号，每个患者每次入科号码唯一)
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 床位号码
        /// </summary>
        public string BedNum { get; set; }

        /// <summary>
        /// 换床时间
        /// </summary>
        public DateTime RecordTime { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        public string NurseCode { get; set; }
    }
}
