using System;
using System.Collections.Generic;
using YiJian.ECIS.ShareModel.HisDto;

namespace YiJian.DoctorsAdvices.Dto
{
    /// <summary>
    /// 添加检查去重提示信息
    /// </summary>
    public class AddPacsResponseDto
    {
        /// <summary>
        /// 原guidList
        /// </summary>
        public List<Guid> GuidList { set; get; }

        /// <summary>
        /// 调用检查去重之后his返回的信息
        /// </summary>
        public List<AddPacsHisResponse> AddPacsHisResponses { set; get; }
    }

    /// <summary>
    /// 提交检查去重之后his返回的处置方法list
    /// </summary>
    public class AddPacsHisResponse
    {
        /// <summary>
        /// 处置类型方法
        /// </summary>
        public CzffEnum CzffEnum { get; set; }

        /// <summary>
        /// 医嘱Id
        /// </summary>
        public Guid DoctorsAdviceId { get; set; }

        /// <summary>
        /// 检查细项Id
        /// </summary>
        public Guid PacsItemId { get; set; }

        /// <summary>
        /// 检查项代码
        /// </summary> 
        public string ProjectCode { get; set; }

        /// <summary>
        /// 检查项名称
        /// </summary> 
        public string ProjectName { get; set; }

        /// <summary>
        /// 小项编码
        /// </summary>
        public string TargetCode { get; set; }

        /// <summary>
        /// 小项名称
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// （只有项目替换才会使用）小项编码
        /// </summary>
        public string NewTargetCode { get; set; }

        /// <summary>
        /// （只有项目替换才会使用）小项名称
        /// </summary>
        public string NewTargetName { get; set; }

        /// <summary>
        /// 单价
        /// </summary> 
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary> 
        public int Qty { get; set; }

        /// <summary>
        /// （只有项目替换才会使用）单价
        /// </summary> 
        public decimal NewPrice { get; set; }

        /// <summary>
        /// （只有项目替换才会使用）数量
        /// </summary> 
        public decimal NewQty { get; set; }
    }
}
