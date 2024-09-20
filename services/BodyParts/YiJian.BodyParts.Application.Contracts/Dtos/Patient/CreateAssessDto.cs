using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace YiJian.BodyParts.Dtos
{
    public class CreateAssessDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 患者id
        /// </summary>
        public string PI_ID { get; set; }

        /// <summary>
        /// 评估时间
        /// </summary>
        public DateTime AssessTime { get; set; }

        /// <summary>
        /// 护士编号
        /// </summary>
        public string NurseCode { get; set; }

        /// <summary>
        /// 护士名称
        /// </summary>
        public string NurseName { get; set; }

        /// <summary>
        /// 提示内容
        /// </summary>
        public string[] PromptContent { get; set; }

        /// <summary>
        /// 是否特殊交班内容
        /// </summary>
        public bool SpcialContent { get; set; }

        /// <summary>
        /// 护理问题
        /// </summary>
        public List<NursingProblem> NursingProblem { get; set; }

        public List<CreateAssessDetailDto> detailDtos { get; set; }
    }

    public class NursingProblem
    {
        public string id { get; set; }

        public string designation { get; set; }
    }

    public class CreateAssessDetailDto
    {
        /// <summary>
        /// 评估代码
        /// </summary>
        public string Pcode { get; set; }

        /// <summary>
        /// 评估名称
        /// </summary>
        public string Pname { get; set; }

        /// <summary>
        /// 评估值
        /// </summary>
        public string Pvalue { get; set; }

        /// <summary>
        /// 文本值
        /// </summary>
        public string Tvalue { get; set; }

        /// <summary>
        /// 评分Id
        /// </summary>
        public string ScoreId { get; set; }
    }
}
