using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using JetBrains.Annotations;

namespace YiJian.BodyParts.Application.Contracts.Dtos.Patient
{
    public class TsePatientRemarksDto
    {

        /// <summary>
        /// 患者id
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string PI_ID { get; set; }


        /// <summary>
        /// 标签内容
        /// </summary>
        [StringLength(500)]
        [CanBeNull]
        public string Content { get; set; }
    }
    public class TePatientRemarksDto
    {


        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string PI_ID { get; set; }


        /// <summary>
        /// 标签内容
        /// </summary>
        [StringLength(500)]
        [CanBeNull]
        public string Content { get; set; }
    }
    public class PatientRemarksDto
    {


        /// <summary>
        /// 
        /// </summary>
        public Guid? Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        [CanBeNull]
        [StringLength(20)]
        public string PI_ID { get; set; }


        /// <summary>
        /// 标签名称
        /// </summary>
        [StringLength(150)]
        [CanBeNull]
        public string Lable { get; set; }


        /// <summary>
        /// 标签内容
        /// </summary>
        [StringLength(500)]
        [CanBeNull]
        public string Content { get; set; }
    }
    public class PatientRemarkslistDto
    {
       
        /// <summary>
        /// 标签名称
        /// </summary>
        [StringLength(150)]
        [CanBeNull]
        public string Lable { get; set; }


        /// <summary>
        /// 标签内容
        /// </summary>
        [StringLength(500)]
        [CanBeNull]
        public string Content { get; set; }
    }
}
