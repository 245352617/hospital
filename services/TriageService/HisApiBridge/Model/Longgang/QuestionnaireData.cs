using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 问卷调查数据
    /// </summary>
    public class QuestionnaireData
    {
        /// <summary>
        /// 类型：Array  必有字段  备注：问卷问题列表
        /// </summary>
        [JsonProperty(PropertyName = "answers")]
        public List<QuestionModel> Questions { get; set; }

        /// <summary>
        /// 类型：Object  必有字段  备注：用户问卷填写信息
        /// </summary>
        [JsonProperty(PropertyName = "data")]
        public AnswerData Data { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：医生签名
        /// </summary>
        [JsonProperty(PropertyName = "doc_sign")]
        public string DoctorSign { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：用户签名
        /// </summary>
        /// <example>data:image/png;base64,iVBORwkQCIE</example>
        [JsonProperty(PropertyName = "sign")]
        public string Sign { get; set; }
    }

    /// <summary>
    /// 用户问卷填写信息
    /// </summary>
    public class AnswerData
    {
        /// <summary>
        /// 类型：Number  必有字段  备注：答卷ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 类型：varchar  必有字段  备注：就诊卡号
        /// </summary>
        [JsonProperty(PropertyName = "mzhm")]
        public string MZHM { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：用户ID
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：姓名
        /// </summary>
        [JsonProperty(PropertyName = "user_name")]
        public string UserName { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：用户类型 1本人 2其他
        /// </summary>
        [JsonProperty(PropertyName = "user_type")]
        public int UserType { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：性别 0男1女
        /// </summary>
        [JsonProperty(PropertyName = "sex")]
        public int Sex { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：手机号
        /// </summary>
        [JsonProperty(PropertyName = "phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：证件号码
        /// </summary>
        [JsonProperty(PropertyName = "card")]
        public string IdCardNo { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：证件类型，01居民身份证，03护照，04军官证，06港澳居民来往内地通行证，07台湾居民来往内地通行证
        /// </summary>
        [JsonProperty(PropertyName = "id_type")]
        public string IdType { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：体温状态 0:正常 1:发热
        /// </summary>
        [JsonProperty(PropertyName = "temperature")]
        public int Temperature { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：体温温度
        /// </summary>
        [JsonProperty(PropertyName = "temperatureVal")]
        public string TemperatureVal { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：医院ID
        /// </summary>
        [JsonProperty(PropertyName = "unit_id")]
        public int UnitId { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：渠道ID
        /// </summary>
        [JsonProperty(PropertyName = "cid")]
        public int CId { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：监护人姓名
        /// </summary>
        [JsonProperty(PropertyName = "guardian_name")]
        public string GuardianName { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：监护人证件号
        /// </summary>
        [JsonProperty(PropertyName = "guardian_card")]
        public string GuardianCardNo { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：订单编号
        /// </summary>
        [JsonProperty(PropertyName = "order_no")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：问卷答案值
        /// </summary>
        /// <example>[{"id":1,"is_must":1,"qCode":"1","value":["2"],"blanks":"","qtype":"1","optdesc":[{"aValue":"2","optText":"玉林"}]}]</example>
        [JsonProperty(PropertyName = "health_value")]
        public List<AnswerModel> HealthValue { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：健康码编号
        /// </summary>
        [JsonProperty(PropertyName = "health_code")]
        public string HealthCode { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：健康码颜色0未知1绿色2橙色3红色
        /// </summary>
        [JsonProperty(PropertyName = "health_type")]
        public int HealthType { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：健康码颜色原因说明
        /// </summary>
        [JsonProperty(PropertyName = "color_remark")]
        public string ColorRemark { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：创建时间
        /// </summary>
        /// <example>2022-04-09 09:26:42</example>
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：创建时间
        /// </summary>
        /// <example>2022-04-09 09:26:42</example>
        [JsonProperty(PropertyName = "created_str")]
        public string CreatedStr { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：有效期
        /// </summary>
        /// <example>2022-04-10 09:26:42</example>
        [JsonProperty(PropertyName = "validity_date")]
        public string ValidityDate { get; set; }

        /// <summary>
        /// 申请日期
        /// </summary>
        /// <example>2021-12-07 09-09-05</example>
        [JsonProperty(PropertyName = "to_date")]
        public string ToDate { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：有效期天数
        /// </summary>
        [JsonProperty(PropertyName = "validity_num")]
        public int ValidityNum { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：有效期描述
        /// </summary>
        [JsonProperty(PropertyName = "validity_date_str")]
        public string ValidatyDateStr { get; set; }

        /// <summary>
        /// 类型：Number  必有字段  备注：问卷模板ID
        /// </summary>
        [JsonProperty(PropertyName = "questionnaire_id")]
        public int QuestionnaireId { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：县区编码
        /// </summary>
        [JsonProperty(PropertyName = "area_code")]
        public string AreaCode { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：城市名
        /// </summary>
        [JsonProperty(PropertyName = "city_name")]
        public string CityName { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：地址
        /// </summary>
        [JsonProperty(PropertyName = "address")]
        public string Address { get; set; }

        /// <summary>
        /// 类型：String  必有字段  备注：就诊日期
        /// </summary>
        /// <example>2022-04-08</example>
        [JsonProperty(PropertyName = "visit_date")]
        public string VisitDate { get; set; }
    }

    /// <summary>
    /// 问卷答案
    /// </summary>
    public class AnswerModel
    {
        /// <summary>
        /// 问题ID
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }

        /// <summary>
        /// 选中选项编号数组
        /// </summary>
        [JsonProperty(PropertyName = "value")]
        public List<string> Value { get; set; }

        /// <summary>
        /// 选中选项编号数组
        /// </summary>
        [JsonProperty(PropertyName = "optdesc")]
        public List<OptionDescription> OptionDescriptions { get; set; }
    }

    /// <summary>
    /// 问题的选项附加填空列表
    /// </summary>
    public class OptionDescription
    {
        /// <summary>
        /// 选项编号
        /// </summary>
        [JsonProperty(PropertyName = "aValue")]
        public string OptionValue { get; set; }

        /// <summary>
        /// 选项附带填空内容
        /// </summary>
        [JsonProperty(PropertyName = "optText")]
        public string OptionText { get; set; }
    }
}
