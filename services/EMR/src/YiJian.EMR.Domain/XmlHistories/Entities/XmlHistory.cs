using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.EMR.Enums;

namespace YiJian.EMR.XmlHistories.Entities
{
    /// <summary>
    /// 电子病历留痕实体
    /// </summary>
    [Comment("电子病历留痕实体")]
    public class XmlHistory : FullAuditedAggregateRoot<Guid>
    {
        private XmlHistory()
        {

        }

        public XmlHistory(Guid id, 
            Guid xmlId, 
            EXmlCategory xmlCategory, 
            [NotNull] string emrXml, 
            string doctorCode, 
            string doctorName)
        {
            Id  = id;
            XmlId = xmlId;
            XmlCategory = xmlCategory;
            EmrXml = emrXml; 
            DoctorCode = doctorCode;
            DoctorName = doctorName;
        }
          
        /// <summary>
        /// 患者电子病历的XML文档Id
        /// </summary> 
        [Required(ErrorMessage = "患者电子病历的XML文档Id不能为空")]
        public Guid XmlId { get; set; }

        /// <summary>
        /// 电子病历Xml文档
        /// </summary> 
        [Required(ErrorMessage = "电子病历留痕的Xml文档不能为空")]
        public string EmrXml { get; set; }

        /// <summary>
        /// xml 电子病例模板类型(0=电子病历库的模板，1=我的电子病历模板的模板，2=已存档的患者电子病历)
        /// </summary>
        [Required(ErrorMessage = "电子病历留痕的Xml文档不能为空")]
        public EXmlCategory XmlCategory { get; set; }
         
        /// <summary>
        /// 医生编号
        /// </summary>
        [Comment("医生编号")]
        [Required(ErrorMessage = "医生编号不能为空"), StringLength(32, ErrorMessage = "医生编号最大长度32字符")]
        public string DoctorCode { get; set; }

        /// <summary>
        /// 医生名称
        /// </summary>
        [Comment("医生名称")]
        [Required(ErrorMessage = "医生名称不能为空"), StringLength(50, ErrorMessage = "医生名称最大长度50字符")]
        public string DoctorName { get; set; }
          
        //public void Update([NotNull] string emrXml)
        //{
        //    EmrXml = emrXml;
        //}

    }
}
