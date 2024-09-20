using System;
using System.Collections.Generic;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    /// <summary>
    /// 文书管理-危重患者转运单Dto
    /// </summary>
    public class PatientTransportDto
    {
        public string id { get; set; }

        /// <summary>
        /// 文书名称
        /// </summary>
        public string scoreName { get; set; }
        /// <summary>
        /// 文书对应编码
        /// </summary>
        public string scoreCode { get; set; }
             
        /// <summary>
        /// 患者流水号
        /// </summary>
        public string inhosum { get; set; }

        /// <summary>
        /// 患者姓名
        /// </summary>
        public string patientName { get; set; }

        /// <summary>
        /// 性别
        /// </summary>
        public string patientGender { get; set; }

        /// <summary>
        /// 年龄
        /// </summary>
        public string patientAge { get; set; }

        /// <summary>
        /// 住院号
        /// </summary>
        public string inHosId { get; set; }

        /// <summary>
        /// 转运时间 "yyyy-MM-dd HH:mm"
        /// </summary>
        public string transportTime { get; set; }

        /// <summary>
        /// 入科诊断
        /// </summary>
        public string indiagnosis { get; set; }

        /// <summary>
        /// 模板备注
        /// </summary>
        public string remark { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string patientRemark { get; set; }

        /// <summary>
        /// 评分项目和得分集
        /// </summary>
        public ParaScore paraScore { get; set; } = new ParaScore();
        /// <summary>
        /// 措施
        /// </summary>
        public MeasureDetail measureDetail { get; set; } = new MeasureDetail();
        /// <summary>
        /// 总分
        /// </summary>
        public decimal? totalScore { get; set; }
    }

    public class MeasureDetail
    {
        /// <summary>
        /// 选项
        /// </summary>
        public IEnumerable<string> options { get; set; } = new List<string>();
        /// <summary>
        /// 选中项
        /// </summary>
        public IEnumerable<string> selectOptions { get; set; } = new List<string>();
        /// <summary>
        /// 其他 对应的值
        /// </summary>
        public string otherValue { get; set; }

    }

    /// <summary>
    /// 评分项目和得分集
    /// </summary>
    public class ParaScore
    {
        /// <summary>
        /// 项目列表，如生命体征等
        /// </summary>
        public string[] paraNameList { get; set; } = new string[0];

        /// <summary>
        ///  选项、得分 =》 动态
        /// </summary>
        public List<string[]> options { get; set; } = new List<string[]>();

    }

    public class PatientTransportResponseDto : PatientTransportDto
    {
        /// <summary>
        /// 是否为记录
        /// </summary>
        public bool isRecord { get; set; }

        public string signatureSignImage { get; set; }

        public string SignNurseName { get; set; }

        public string SignNurseCode { get; set; }

        public string signatureSignImage2 { get; set; }

        public string SignNurseName2 { get; set; }

        public string SignNurseCode2 { get; set; }
    }

}
