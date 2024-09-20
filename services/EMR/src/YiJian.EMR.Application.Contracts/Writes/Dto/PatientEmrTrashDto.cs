using System;
using Volo.Abp.Application.Dtos;
using YiJian.EMR.Enums;

namespace YiJian.EMR.Writes.Dto
{
    /// <summary>
    /// 回收站上电子病历
    /// </summary>
    public class PatientEmrTrashDto:EntityDto<Guid>
    {
        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者编号
        /// </summary>   
        public string PatientNo { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary>   
        public string PatientName { get; set; }
          
        /// <summary>
        /// 医生编号
        /// </summary>   
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>   
        public string DoctorName { get; set; }

        /// <summary>
        /// 病历名称
        /// </summary>   
        public string Title { get; set; }
              
        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）
        /// </summary>
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public virtual DateTime? DeletionTime { get; set; }

    }

}
