using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 返回文档DTO
    /// </summary>
    public class RulesRegulationsFileListDto : EntityDto<Guid>
    {
        /// <summary>
        /// 文件名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 文件后缀（类型）
        /// </summary>
        public string FileSuffix { get; set; }

        /// <summary>
        /// 桶名称，如果文件同时在多个桶中，则英文逗号相隔
        /// </summary>
        public string BucketName { get; set; }

        /// <summary>
        /// 上传时间
        /// </summary>
        public DateTime UploadTime { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDel { get; set; }

        /// <summary>
        /// 删除时间
        /// </summary>
        public DateTime? DelTime { get; set; }
    }
}
