using System;
using System.Collections.Generic;

namespace YiJian.Documents.Dto
{
    /// <summary>
    /// GRPC返回的数据
    /// </summary>
    public class SeparationDto
    {
        /// <summary>
        /// 分方配置Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 标题名称
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 分方单分类编码，0=注射单，1=输液单，2=雾化单...
        /// </summary> 
        public int Code { get; set; }

        /// <summary>
        /// 用药途径
        /// </summary>
        public List<UsageDto> Usages { get; set; } = new List<UsageDto>();

    }

}
