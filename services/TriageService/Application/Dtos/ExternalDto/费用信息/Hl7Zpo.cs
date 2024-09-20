namespace SamJan.MicroService.PreHospital.TriageService
{
    /**
     * 门诊已缴费卡交易段(Hl7Zpo)实体类
     *
     * @author cjd
     * @since 2020-08-28 14:52:28
     */
    public class Hl7Zpo
    {
        /// <summary>
        ///门诊号码
        /// </summary>
        public string mzhm {get;set;}

        /// <summary>
        ///发票号
        /// </summary>
        public string fph {get;set;}

        /// <summary>
        ///医保卡号
        /// </summary>
        public string ybkh {get;set;}

        /// <summary>
        ///门诊日期
        /// </summary>
        public string mzrq {get;set;}

        /// <summary>
        ///处方单号
        /// </summary>
        public string cfsb {get;set;}

        /// <summary>
        ///病人姓名
        /// </summary>
        public string brxm {get;set;}

        /// <summary>
        ///收费类别
        /// </summary>
        public string sflb {get;set;}

        /// <summary>
        ///记账金额
        /// </summary>
        public string jzje {get;set;}

        /// <summary>
        ///个人缴费
        /// </summary>
        public string grjf {get;set;}

        /// <summary>
        ///结算方式
        /// </summary>
        public string jsfs {get;set;}

        /// <summary>
        ///发药药房
        /// </summary>
        public string fyyf {get;set;}

        /// <summary>
        ///发药窗口
        /// </summary>
        public string fyck {get;set;}

        /// <summary>
        ///科室位置
        /// </summary>
        public string kswz {get;set;}

        /// <summary>
        ///西药费
        /// </summary>
        public string xyf {get;set;}

        /// <summary>
        ///中成药
        /// </summary>
        public string zcy {get;set;}

        /// <summary>
        ///中草药
        /// </summary>
        public string zcay {get;set;}

        /// <summary>
        ///诊查费
        /// </summary>
        public string zcf {get;set;}

        /// <summary>
        ///检验费
        /// </summary>
        public string jyf {get;set;}

        /// <summary>
        ///检查费
        /// </summary>
        public string jcf {get;set;}

        /// <summary>
        ///放射费
        /// </summary>
        public string fsf {get;set;}

        /// <summary>
        ///治疗费
        /// </summary>
        public string zlf {get;set;}

        /// <summary>
        ///手术费
        /// </summary>
        public string ssf {get;set;}

        /// <summary>
        ///械字材料
        /// </summary>
        public string xzcl {get;set;}

        /// <summary>
        ///其他费
        /// </summary>
        public string qtf {get;set;}

        /// <summary>
        ///特需服务费
        /// </summary>
        public string txfwf {get;set;}

        /// <summary>
        ///急诊留观床位费
        /// </summary>
        public string jzlgcwf {get;set;}

        /// <summary>
        ///CT费
        /// </summary>
        public string ct {get;set;}

        /// <summary>
        ///输血费
        /// </summary>
        public string sxf {get;set;}

        /// <summary>
        ///输氧费
        /// </summary>
        public string syf {get;set;}

        /// <summary>
        ///医材费
        /// </summary>
        public string ycf {get;set;}

        /// <summary>
        ///护理费
        /// </summary>
        public string hlf {get;set;}

        /// <summary>
        ///舍入
        /// </summary>
        public string sr {get;set;}

        /// <summary>
        ///总金额
        /// </summary>
        public string zje {get;set;}

        /// <summary>
        ///账户信息
        /// </summary>
        public string zhxx {get;set;}

        /// <summary>
        ///收费员
        /// </summary>
        public string sfy {get;set;}

        /// <summary>
        ///材料费
        /// </summary>
        public string clf {get;set;}

        /// <summary>
        ///订单号
        /// </summary>
        public string ddh {get;set;}
    }
}