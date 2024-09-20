using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.BodyParts.Model
{
    /// <summary>
    /// 表:签名
    /// </summary>
    public class IcuSignature : Entity<Guid>
    {
        public IcuSignature() { }

        public IcuSignature(Guid id) : base(id) { }

        public void SetId(Guid SignatureId)
        {
            this.Id = SignatureId;
        }

        /// <summary>
        /// 患者id
        /// </summary>
        [StringLength(50)]
        public string PI_ID { get; set; }

        /// <summary>
        /// 护理时间
        /// </summary>
        [Required]
        public DateTime NurseTime { get; set; }

        /// <summary>
        /// 签名工号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string SignNurseCode { get; set; }

        /// <summary>
        /// 签名名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string SignNurseName { get; set; }

        /// <summary>
        /// 签名时间
        /// </summary>

        public DateTime? SignTime { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        [CanBeNull]
        public string SignImage { get; set; }

        /// <summary>
        /// 签名标志
        /// </summary>
        public int? SignState { get; set; }

        /// <summary>
        /// 二级签名工号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string SignNurseCode2 { get; set; }

        /// <summary>
        /// 二级签名名称
        /// </summary>
        [CanBeNull]
        [StringLength(40)]
        public string SignNurseName2 { get; set; }

        /// <summary>
        /// 二级签名时间
        /// </summary>

        public DateTime? SignTime2 { get; set; }

        /// <summary>
        /// 二级签名图片
        /// </summary>
        [CanBeNull]
        public string SignImage2 { get; set; }

        /// <summary>
        /// 二级签名标志
        /// </summary>
        public int? SignState2 { get; set; }

        /// <summary>
        /// 三级签名工号
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string SignNurseCode3 { get; set; }

        /// <summary>
        /// 三级签名名称
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string SignNurseName3 { get; set; }

        /// <summary>
        /// 三级签名时间
        /// </summary>

        public DateTime? SignTime3 { get; set; }

        /// <summary>
        /// 三级签名图片
        /// </summary>
        [CanBeNull]
        public string SignImage3 { get; set; }

        /// <summary>
        /// 三级签名标志
        /// </summary>
        public int? SignState3 { get; set; }

        /// <summary>
        /// 特护单审核状态（S:已审核）
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string ReviewState { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }

    }
}
