namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 建档、挂号调用接口入参
    /// </summary>
    public class PatientReqDto
    {
        /// <summary>
        /// 社保卡号
        /// </summary>
        public string sscno { get; set; }

        /// <summary>
        /// 患者证件类型
        /// </summary>
        public string patIdType { get; set; }

        /// <summary>
        /// 患者病历号
        /// </summary>
        public string patientId { get; set; }

        /// <summary>
        ///证件号
        /// </summary>
        public string idNo { get; set; }

        /// <summary>
        ///证件类型
        /// </summary>
        public string idType { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string name { get; set; }

        /// <summary>
        ///性别
        /// </summary>
        public string sex { get; set; }

        /// <summary>
        ///出生日期
        /// </summary>
        public string birthday { get; set; }

        /// <summary>
        ///手机号码
        /// </summary>
        public string phone { get; set; }

        /// <summary>
        ///订单号
        /// </summary>
        public string orderNum { get; set; }

        /// <summary>
        ///订单时间
        /// </summary>
        public string orderTime { get; set; }

        #region 预约信息段

        /// <summary>
        ///日程表ID
        /// </summary>
        public string no { get; set; }

        /// <summary>
        ///科室编码
        /// </summary>
        public string deptId { get; set; }

        /// <summary>
        ///科室名称
        /// </summary>
        public string deptName { get; set; }

        /// <summary>
        ///医生编码
        /// </summary>
        public string doctorId { get; set; }

        /// <summary>
        ///看诊日期
        /// </summary>
        public string seeDate { get; set; }

        /// <summary>
        ///开始时间
        /// </summary>
        public string beginTime { get; set; }

        /// <summary>
        ///结束时间
        /// </summary>
        public string endTime { get; set; }

        /// <summary>
        ///挂号级别代码
        /// </summary>
        public string reglevlCode { get; set; }

        /// <summary>
        ///挂号级别名称
        /// </summary>
        public string reglevlName { get; set; }

        /// <summary>
        ///午别（1：上午，2：下午，3：晚上）
        /// </summary>
        public string noonId { get; set; }

        /// <summary>
        ///是否医保（1 是  0 否）
        /// </summary>
        public string insurance { get; set; }

        #endregion 预约信息段

        #region 取消预挂号

        /// <summary>
        ///患者类别
        /// </summary>
        public string patientClass { get; set; }

        /// <summary>
        /// 门诊号码
        /// </summary>
        public string visitNo { get; set; }

        /// <summary>
        ///就诊流水号
        /// </summary>
        public string visitNum { get; set; }

        #endregion 取消预挂号

        /// <summary>
        /// 卡类型
        /// </summary>
        public string cardType { get; set; }

        /// <summary>
        /// 卡号
        /// </summary>
        public string cardNo { get; set; }

        /// <summary>
        /// 国籍
        /// </summary>
        public string nationality { get; set; }

        /// <summary>
        /// 民族
        /// </summary>
        public string ethnicGroup { get; set; }

        /// <summary>
        /// 家庭地址
        /// </summary>
        public string homeAddress { get; set; }

        /// <summary>
        /// 家庭电话号码
        /// </summary>
        public string phoneNumberHome { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string contactName { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string contactPhone { get; set; }

        /// <summary>
        /// 体重
        /// </summary>
        public string weight { get; set; }

        /// <summary>
        /// 登记窗口
        /// </summary>
        public string siteCode { get; set; }

        /// <summary>
        /// 操作员  加@防止与保留关键字冲突
        /// </summary>
        public string @operator { get; set; }

        /// <summary>
        /// 操作员编码
        /// </summary>
        public string operatorCode { get; set; }

        /// <summary>
        /// 操作员姓名
        /// </summary>
        public string operatorName { get; set; }

        /// <summary>
        /// 人群编码
        /// </summary>
        public string crowdCode { get; set; }

        /// <summary>
        /// 联系人姓名
        /// </summary>
        public string associationName { get; set; }

        /// <summary>
        /// 联系人证件类型
        /// </summary>
        public string associationIdType { get; set; }

        /// <summary>
        /// 联系人证件号码
        /// </summary>
        public string associationIdNo { get; set; }

        /// <summary>
        /// 与联系人关系
        /// </summary>
        public string societyRelation { get; set; }

        /// <summary>
        /// 联系人电话
        /// </summary>
        public string associationPhone { get; set; }

        /// <summary>
        /// 联系人地址
        /// </summary>
        public string associationAddress { get; set; }

        /// <summary>
        /// 医保类别(医保)
        /// </summary>
        public string safetyType { get; set; }

        /// <summary>
        /// 医保号/人员编号(医保)
        /// </summary>
        public string safetyNo { get; set; }

        /// <summary>
        /// 就诊卡类型
        /// </summary>
        public string icCardType { get; set; }

        /// <summary>
        /// 就诊卡号
        /// </summary>
        public string icCardNo { get; set; }

        /// <summary>
        /// 费用类别(医保)   1医保 2自费
        /// </summary>
        public string chargeType { get; set; }

        /// <summary>
        /// 挂号类型
        /// 1：普通号；2专家号；3名专家号;4.免费号 
        /// </summary>
        public string regType { get; set; }
        /// <summary>
        /// 挂号方式
        /// 1现场窗口挂号，2现场自助机挂号，3外部预约，4预约挂号，5普通挂号，6特诊(特需门诊)，7其他，8绿色通过，99未知
        /// </summary>
        public string regWay { get; set; }

        /// <summary>
        /// 记账类型
        /// </summary>
        public string bookKeepingUnit { get; set; }

        /// <summary>
        /// 参保地医保区划
        /// </summary>
        public string insuplcAdmdvs { get; set; }

        /// <summary>
        /// 险种类型（社保1101接口insutype字段）
        /// </summary>
        public string insuType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string emergFlag { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string returnVisitFlag { get; set; }

        /// <summary>
        /// 就诊类型  1.叫号队列  2.非队列挂号
        /// </summary>
        public string patientType { get; set; } = "1";

    }
}