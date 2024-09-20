using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YJHealth.MedicalInsurance;
using YJHealth.MedicalInsurance.Basic.Person;
using YJHealth.MedicalInsurance.Contracts;
using YJHealth.MedicalInsurance.Contracts.Ext._90100;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class Insurance
    {

        private static readonly string recer_sys_code = "70600";

        /// <summary>
        /// 省医保【人员信息】（1101）
        /// </summary>
        /// <param name="mdtrt_cert_type">
        /// 就诊凭证类型
        /// “01”电子凭证令牌，“02”身份证，“03”社会保障卡</param>
        /// <param name="mdtrt_cert_no">
        /// 就诊凭证编号
        /// “01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
        /// </param>
        /// <param name="insuplcAdmdv">医保参保地</param>
        /// <returns></returns>
        public static async Task<ResponseMessage<BasicResponse>> GetBasicInfo(string mdtrt_cert_type, string mdtrt_cert_no, string insuplcAdmdv = "440300")
        {
            var basicRequest = BasicRequest.Create(
                data: InputBasic.Create( //人员基本信息获取输入
                    mdtrt_cert_type: mdtrt_cert_type, //就诊凭证类型  此为标识
                    mdtrt_cert_no: mdtrt_cert_no, //就诊凭证编号 就诊凭证类型为“01”时填写电子凭证令牌，为“02”时填写身份证号，为“03”时填写社会保障卡卡号
                    card_sn: "", //卡识别码 就诊凭证类型为“03”时必填
                    begntime: null, //开始时间 获取历史参保信息时传入
                    psn_cert_type: "", //人员证件类型  此为标识
                    certno: "", //证件号码
                    psn_name: "" //人员姓名
                    )
            );
            var requestUri = URLHelper.Instance.GetUrl("1101");

            var request = RequestFactory.Create(
                infno: "1101", // 交易编号 交易编号详见接口列表
                msgid: GetMsgId(), // 发送方报文ID 定点医药机构编号(12)+时间(14)+顺序号(4) 时间格式：yyyyMMddHHmmss
                mdtrtarea_admvs: "440300", // 就医地医保区划
                insuplc_admdvs: insuplcAdmdv, // 参保地医保区划 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
                recer_sys_code: recer_sys_code, // 接收方系统代码 用于多套系统接入，区分不同系统使用
                dev_no: "76107", // 设备编号
                dev_safe_info: "", // 设备安全信息
                cainfo: "", // 数字签名信息
                signtype: "", // 签名类型 建议使用SM2、SM3
                infver: "V1.0", // 接口版本号 例如：“V1.0”，版本号由医保下发通知。
                opter_type: "1", // 经办人类别 1-经办人；2-自助终端；3-移动终端 此为标识
                opter: "szyjian", // 经办人 按地方要求传入经办人/终端编号
                opter_name: "尚哲医健", // 经办人姓名 按地方要求传入经办人姓名/终端名称
                inf_time: DateTime.Now, // 交易时间
                fixmedins_code: MedicalInsuranceProxy.Options.OrganizationCode, // 定点医药机构编号
                fixmedins_name: MedicalInsuranceProxy.Options.Organization, // 定点医药机构名称
                sign_no: "", // 交易签到流水号 通过签到【9001】交易获取
                request: basicRequest // 人员基本信息获取输入
            );

            ResponseMessage<BasicResponse> response = await MedicalInsuranceProxy.ExecuteAsync<BasicRequest, BasicResponse>(requestUri, request);

            return response;
        }

        /// <summary>
        /// 省医保【90100-缴费查询】（龙岗）
        /// </summary>
        /// <param name="psn_no">人员编号</param>
        /// <param name="insuplc_admdvs">参保地医保区划</param>
        /// <returns></returns>
        public static async Task<ResponseMessage<PayQueryResponse>> GetPayQueryInfo(string psn_no, string insuplc_admdvs = "440300")
        {
            var basicRequest = PayQueryRequest.Create(
                    data: InputPayQuery.Create(psn_no: psn_no)
            );
            var requestUri = URLHelper.Instance.GetUrl("90100");

            var request = RequestFactory.Create(
                infno: "90100", // 交易编号 交易编号详见接口列表
                msgid: GetMsgId(), // 发送方报文ID 定点医药机构编号(12)+时间(14)+顺序号(4) 时间格式：yyyyMMddHHmmss
                mdtrtarea_admvs: "440300", // 就医地医保区划
                insuplc_admdvs: insuplc_admdvs, // 参保地医保区划 如果交易输入中含有人员编号，此项必填，可通过【1101】人员信息获取交易取得
                recer_sys_code: recer_sys_code, // 接收方系统代码 用于多套系统接入，区分不同系统使用
                dev_no: "76107", // 设备编号
                dev_safe_info: "", // 设备安全信息
                cainfo: "", // 数字签名信息
                signtype: "", // 签名类型 建议使用SM2、SM3
                infver: "V1.0", // 接口版本号 例如：“V1.0”，版本号由医保下发通知。
                opter_type: "1", // 经办人类别 1-经办人；2-自助终端；3-移动终端 此为标识
                opter: "Longgang", // 经办人 按地方要求传入经办人/终端编号
                opter_name: "深圳市龙岗中心医院", // 经办人姓名 按地方要求传入经办人姓名/终端名称
                inf_time: DateTime.Now, // 交易时间
                fixmedins_code: MedicalInsuranceProxy.Options.OrganizationCode, // 定点医药机构编号
                fixmedins_name: MedicalInsuranceProxy.Options.Organization, // 定点医药机构名称
                sign_no: "", // 交易签到流水号 通过签到【9001】交易获取
                request: basicRequest // 人员基本信息获取输入
            );

            var response = await MedicalInsuranceProxy.ExecuteAsync<PayQueryRequest, PayQueryResponse>(requestUri, request);

            return response;
        }

        private static string GetMsgId()
        {
            return MedicalInsuranceProxy.Options.OrganizationCode + DateTime.Now.ToString("yyyyMMddHHmmss") + new Random().Next(9999).ToString("D4");
        }
    }
}
