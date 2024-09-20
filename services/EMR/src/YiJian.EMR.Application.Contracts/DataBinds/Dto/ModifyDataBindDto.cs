using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;
using YiJian.EMR.Enums;
using YiJian.EMR.Templates.Dto;

namespace YiJian.EMR.DataBinds.Dto
{
    /// <summary>
    /// 需要更新或新增的绑定数据模型
    /// </summary>
    public class ModifyDataBindDto
    {
        /// <summary>
        /// 就诊号
        /// </summary> 
        [Required, StringLength(32)]
        public string VisitNo { get; set; }

        /// <summary>
        /// 流水号
        /// </summary> 
        [Required, StringLength(32)]
        public string RegisterSerialNo { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary> 
        [Required, StringLength(100)]
        public string OrgCode { get; set; }
          
        /// <summary>
        /// 患者唯一Id
        /// </summary> 
        [Required]
        public Guid PI_ID { get; set; }

        /// <summary>
        /// 患者Id
        /// </summary> 
        [Required, StringLength(32)]
        public string PatientId { get; set; }

        /// <summary>
        /// 患者名称
        /// </summary> 
        [Required, StringLength(30)]
        public string PatientName { get; set; }

        /// <summary>
        /// 录入人Id
        /// </summary> 
        [StringLength(32)]
        public string WriterId { get; set; }

        /// <summary>
        /// 录入人名称
        /// </summary> 
        [StringLength(30)]
        public string WriterName { get; set; }

        /// <summary>
        /// 电子文书分类（0=电子病历，1=文书）默认电子病历
        /// </summary> 
        [Required]
        public EClassify Classify { get; set; } = EClassify.EMR;

        /// <summary>
        /// 患者电子病历Id
        /// </summary>  
        public Guid? PatientEmrId { get; set; }

        /// <summary>
        /// 绑定的数据类型, 第一层key是datasource ,第二层key是path
        /// <code>
        /// //实例如下： 第一层如 patient, doctor;第二层 如 patientId, patientName,age,doctorId,doctorName ...
        /// {
        ///     "patient":{
        ///         "patientId":"1000001",
        ///         "patientName":"张三",
        ///         "age":"20"
        ///     },
        ///     "doctor":{
        ///         "doctorId":"0000001",
        ///         "doctorName":"张大大"
        ///     },
        ///     ...
        /// }
        /// </code>
        /// </summary>
        [Required] 
        public Dictionary<string,Dictionary<string,object>> Data { get; set; } = new Dictionary<string, Dictionary<string, object>>();
         
        /// <summary>
        /// 通过配置的setting获取病例指定的数据源
        /// </summary>
        /// <param name="setting"></param>
        /// <returns></returns>
        public PushEmrDataEto GetPushEmrData(PushEmrDataSetting setting)
        {
            if (Data.Count == 0) return null;

            var pastmedicalhistory = GetSettingValue(setting.DataSource, setting.Path.Pastmedicalhistory);//既往史
            var presentmedicalhistory = GetSettingValue(setting.DataSource, setting.Path.Presentmedicalhistory);//现病史 
            var physicalexamination = GetSettingValue(setting.DataSource, setting.Path.Physicalexamination); //体格检查
            var narrationname = GetSettingValue(setting.DataSource, setting.Path.Narrationname); //主诉

            var aidpacs = GetSettingValue(setting.DataSource, setting.Path.Aidpacs); //辅助检查结果
            var treatopinion = GetSettingValue(setting.DataSource, setting.Path.Treatopinion); //处理意见
            var diagnosename = GetSettingValue(setting.DataSource, setting.Path.Diagnosename); //初步诊断
            
            if (pastmedicalhistory.IsNullOrEmpty() && presentmedicalhistory.IsNullOrEmpty() && physicalexamination.IsNullOrEmpty() && narrationname.IsNullOrEmpty())
            {
                return null;
            }
            var eto = new PushEmrDataEto(
                pastmedicalhistory: pastmedicalhistory,
                presentmedicalhistory: presentmedicalhistory,
                physicalexamination: physicalexamination,
                narrationname: narrationname,
                allergySign: "",
                diagnosename: diagnosename,
                treatopinion: treatopinion,
                outpatientSurgery: "",
                aidpacs: aidpacs);

            eto.SetPatientInfo(
                piid: PI_ID,
                patientId: PatientId,
                patientName: PatientName,
                visitNo:VisitNo,
                registerNo:RegisterSerialNo);
             
            return eto;
        }

        /// <summary>
        /// 获取配置信息
        /// </summary>
        /// <param name="datasouce"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        private string GetSettingValue(string datasouce, string path)
        {
            if (Data.Count == 0) return null;

            try
            {
                foreach (var data in Data)
                {
                    foreach (var item in data.Value)
                    {
                        if (item.Key.Trim().ToLower() == path.ToLower())
                        {
                            return item.Value.ToString();
                        }
                    }
                }

                return "";
            }
            catch (Exception)
            {
                return "";
            }
        }

    }
}
