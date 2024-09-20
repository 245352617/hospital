using System;

namespace YiJian.Health.Report.NursingDocuments.Dto
{
    /// <summary>
    /// 护理记录单Sheet
    /// </summary>
    public class SheetDto
    {
        /// <summary>
        /// 护理单Id
        /// </summary>  
        public Guid NursingDocumentId { get; set; }

        /// <summary>
        /// 动态六项的Id，也是sheet分页的Id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 新建页索引
        /// </summary>  
        public int SheetIndex { get; set; }

        /// <summary>
        /// 新建页索引
        /// </summary> 
        public string SheetName { get; set; }
    }

}
