using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace ICIS.Model
{
    /// <summary>
    /// 表：文件记录表
    /// </summary>
    public class FileRecord : Entity<Guid>
    {
        public FileRecord() { }

        public FileRecord(Guid id) : base(id) { }

        /// <summary>
        /// 文件名称
        /// </summary>
        [StringLength(50)]
        [Required]
        public string FileName { get; set; }

        /// <summary>
        /// 文件后缀（类型）
        /// </summary>
        [StringLength(20)]
        [Required]
        public string FileSuffix { get; set; }

        /// <summary>
        /// 桶名称，如果文件同时在多个桶中，则英文逗号相隔
        /// </summary>
        [StringLength(50)]
        [Required]
        public string BucketName { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        [StringLength(20)]
        [CanBeNull]
        public string DeptCode { get; set; }

        /// <summary>
        /// 患者ID
        /// </summary>
        [StringLength(20)]
        [CanBeNull]
        public string PI_ID { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DelTime { get; set; }

        /// <summary>
        /// 文件大小
        /// </summary>
        public long Size { get; set; }

        ///// <summary>
        ///// 文件访问地址
        ///// </summary>
        //public string FileUrl { get; set; }

        /// <summary>
        /// regulation = ，literatur = 文献，skinPicture = 皮肤图片
        /// </summary>
        [StringLength(20)]
        [Required]
        public string ModuleType { get; set; }
    }
}
