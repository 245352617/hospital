using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理记录单Sheet设置
    /// </summary>
    public class SheetSetDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 护理单Id(新增的时候需要传过来，修改的时候可以不传)
        /// </summary>  
        public Guid? NursingDocumentId { get; set; }

        /// <summary>
        /// 新建页索引名称
        /// </summary> 
        [StringLength(50, ErrorMessage = "新建页索引名称字符不能超过50字")]
        public string SheetName { get; set; }

    }

}
