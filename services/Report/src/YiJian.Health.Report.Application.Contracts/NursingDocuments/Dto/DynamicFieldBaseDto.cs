using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 动态字段Dto
    /// </summary>
    public class DynamicFieldBaseDto : EntityDto<Guid?>
    {
        /// <summary>
        /// 保留字段1
        /// </summary>  
        public Guid? Field1 { get; set; }

        /// <summary>
        /// 保留字段2
        /// </summary>  
        public Guid? Field2 { get; set; }

        /// <summary>
        /// 保留字段3  
        /// </summary>  
        public Guid? Field3 { get; set; }

        /// <summary>
        /// 保留字段4 
        /// </summary>  
        public Guid? Field4 { get; set; }

        /// <summary>
        /// 保留字段5 
        /// </summary>  
        public Guid? Field5 { get; set; }

        /// <summary>
        /// 保留字段6  
        /// </summary>  
        public Guid? Field6 { get; set; }

        /// <summary>
        /// 保留字段7
        /// </summary>  
        public Guid? Field7 { get; set; }

        /// <summary>
        /// 保留字段8
        /// </summary>  
        public Guid? Field8 { get; set; }

        /// <summary>
        /// 保留字段9
        /// </summary>  
        public Guid? Field9 { get; set; }


        /// <summary>
        /// 护理单Id
        /// </summary> 
        [Required]
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary> 
        [Required]
        public int SheetIndex { get; set; }

        private string sheetName;
        /// <summary>
        /// 新建页索引名称
        /// </summary> 
        [StringLength(50, ErrorMessage = "新建页索引名称字符不能超过50字")]
        public string SheetName
        {
            get { return sheetName; }
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    sheetName = $"护理记录单{SheetIndex + 1}";
                }
                else
                {
                    sheetName = value;
                }
            }
        }

    }

}
