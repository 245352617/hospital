using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class DocumentSignatureDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 模板编号,Url参数编码
        /// </summary>
        public string TemplateCode { get; set; }

        /// <summary>
        /// 科室代码
        /// </summary>
        public string DeptCode { get; set; }

        /// <summary>
        /// 交班时间
        /// </summary>
        public DateTime HandOverTime { get; set; }

        /// <summary>
        /// 签名工号
        /// </summary>
        public string SignNurseCode { get; set; }

        /// <summary>
        /// 签名名称
        /// </summary>
        public string SignNurseName { get; set; }

        /// <summary>
        /// 签名图片
        /// </summary>
        public string SignImage { get; set; }

        /// <summary>
        /// 二级签名工号
        /// </summary>
        public string SignNurseCode2 { get; set; }

        /// <summary>
        /// 二级签名名称
        /// </summary>
        public string SignNurseName2 { get; set; }

        /// <summary>
        /// 二级签名图片
        /// </summary>
        public string SignImage2 { get; set; }

        /// <summary>
        /// 三级签名工号
        /// </summary>
        public string SignNurseCode3 { get; set; }

        /// <summary>
        /// 三级签名名称
        /// </summary>
        public string SignNurseName3 { get; set; }

        public Guid HandOverId { get; set; }

        /// <summary>
        /// 三级签名图片
        /// </summary>
        public string SignImage3 { get; set; }

        /// <summary>
        /// a1->p1签名工号
        /// </summary>
        public string ASignNurseCode { get; set; }

        /// <summary>
        /// a1->p1签名名称
        /// </summary>
        public string ASignNurseName { get; set; }

        /// <summary>
        /// a1->p1签名图片
        /// </summary>
        public string ASignImage { get; set; }

        /// <summary>
        /// n1->p1签名工号
        /// </summary>
        public string PSignNurseCode { get; set; }

        /// <summary>
        /// n1->p1签名名称
        /// </summary>
        public string PSignNurseName { get; set; }

        /// <summary>
        /// n1->p1签名图片
        /// </summary>
        public string PSignImage { get; set; }

        /// <summary>
        /// p1->a1签名工号
        /// </summary>
        public string NSignNurseCode { get; set; }

        /// <summary>
        /// p1->a1签名名称
        /// </summary>
        public string NSignNurseName { get; set; }

        /// <summary>
        /// p1->a1签名图片
        /// </summary>
        public string NSignImage { get; set; }
    }
}
