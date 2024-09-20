using System.Collections.Generic;

namespace YiJian.Hospitals.Dto
{
    public class ListApplyInfoResponse
    {
        /// <summary>
        /// 申请单号
        /// </summary>
        public string ApplyNo { get; set; }

        /// <summary>
        /// 申请人编号
        /// </summary>
        public string ApplyOperatorCode { get; set; }

        /// <summary>
        /// 申请人姓名
        /// </summary>
        public string ApplyOperatorName { get; set; }

        /// <summary>
        /// 申请科室编号
        /// </summary>
        public string ApplyDeptCode { get; set; }

        /// <summary>
        /// 申请科室名称
        /// </summary>
        public string ApplyDeptName { get; set; }

        /// <summary>
        /// 就诊科室编号
        /// </summary>
        public string VisitDeptCode { get; set; }

        /// <summary>
        /// 就诊科室名称
        /// </summary>
        public string VisitDeptName { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public string ApplyTime { get; set; }

        /// <summary>
        /// 检验项目
        /// </summary>
        public List<ReportMasterItemResponse> MasterItemList { get; set; } = new List<ReportMasterItemResponse>();

    }


}