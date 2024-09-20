using System;
using Volo.Abp.Application.Dtos;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 快速通道Dto
    /// </summary>
    public class FastTrackRegisterInfoDto : EntityDto<Guid>
    {
        /// <summary>
        /// 患者姓名
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }

        /// <summary>
        /// 患者年龄
        /// </summary>
        public string Age { get; set; }
        
        /// <summary>
        /// 所属派出所Id
        /// </summary>
        public Guid PoliceStationId { get; set; }
        
        /// <summary>
        /// 所属派出所
        /// </summary>
        public string PoliceStationName { get; set; }

        /// <summary>
        /// 所属派出所电话
        /// </summary>
        public string PoliceStationPhone { get; set; }

        /// <summary>
        /// 警务人员姓名
        /// </summary>
        public string PoliceName { get; set; }

        /// <summary>
        /// 警务人员警号
        /// </summary>
        public string PoliceCode { get; set; }

        /// <summary>
        /// 接诊护士
        /// </summary>
        public string ReceptionNurse { get; set; }
        
        /// <summary>
        /// 接诊护士名称
        /// </summary>
        public string ReceptionNurseName { get; set; }
        
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? LastModificationTime { get; set; }
    }
}