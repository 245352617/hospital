using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;
using YiJian.Hospitals.Enums;

namespace YiJian.Hospitals.Dto
{
    /// <summary>
    /// 拷贝信息
    /// </summary>
    public class CloneV2Request : EntityDto<Guid>
    {
        /// <summary>
        /// 拷贝信息
        /// </summary>
        public CloneV2Request()
        {

        }

        /// <summary>
        /// 拷贝信息
        /// </summary>
        /// <param name="id"></param>
        public CloneV2Request(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// 皮试选择结果
        /// </summary>
        public ESkinTestSignChoseResult? SkinTestSignChoseResult { get; set; }

        /// <summary>
        /// 限制用药标记 “1.限制用药 2.非限制用药” 出现1.限制用药需要弹出 限制费用提示消息
        /// </summary> 
        public int? LimitType { get; set; }

        /// <summary>
        /// 收费类型
        /// </summary>  
        public ERestrictedDrugs? RestrictedDrugs { get; set; }


    }

    /// <summary>
    /// 复制医嘱增加执行科室编码
    /// </summary>
    public class CloneV2RequestWrap
    {
        /// <summary>
        /// 拷贝信息
        /// </summary>
        public List<CloneV2Request> CloneV2Requests { get; set; }

        /// <summary>
        /// 执行科室编码
        /// </summary>
        public string ExecDeptCode { get; set; }

        /// <summary>
        /// 执行科室名称
        /// </summary>
        public string ExecDeptName { get; set; }

        /// <summary>
        /// 申请科室编码
        /// </summary>
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 申请医生编码
        /// </summary> 
        public string ApplyDoctorCode { get; set; }

        /// <summary>
        /// 申请医生
        /// </summary> 
        public string ApplyDoctorName { get; set; }

        /// <summary>
        /// 患者唯一标识
        /// </summary>
        public Guid? PIID { get; set; }
    }
}
