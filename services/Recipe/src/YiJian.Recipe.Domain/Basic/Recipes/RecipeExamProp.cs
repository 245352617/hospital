using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Domain.Entities;

namespace YiJian.Recipes.Basic
{
    /// <summary>
    /// 医嘱-检查属性
    /// Author: ywlin
    /// Date: 2021-12-04
    /// </summary>
    [Comment("医嘱-检查属性")]
    public class RecipeExamProp : Entity<Guid>
    {
        private RecipeExamProp()
        {

        }

        public RecipeExamProp(Guid id)
        {
            this.Id = id;
        }

        public void SetId(Guid id)
        {
            this.Id = id;
        }

        #region Properties

        /// <summary>
        /// 目录编码
        /// </summary>
        [Required]
        [StringLength(20)]
        [Comment("目录编码")]
        public string CatalogCode { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(200)]
        [Comment("分类名称")]
        public string CatalogName { get; set; }
        /// <summary>
        /// 一级目录编码
        /// </summary>
        [StringLength(20)]
        [Comment("一级目录编码")]
        public string FirstCatalogCode { get; set; }

        /// <summary>
        /// 一级分类名称
        /// </summary>
        [StringLength(200)]
        [Comment("一级分类名称")]
        public string FirstCatalogName { get; set; }
        /// <summary>
        /// 检查部位
        /// </summary>
        [StringLength(50)]
        [Comment("检查部位")]
        public virtual string PartName { get; set; }

        /// <summary>
        /// 检查部位编码
        /// </summary>
        [StringLength(50)]
        [Comment("检查部位编码")]
        public virtual string PartCode { get; set; }

        /// <summary>
        /// 位置编码
        /// </summary>
        [StringLength(20)]
        [Comment("位置编码")]
        public virtual string PositionCode { get; set; }

        /// <summary>
        /// 位置
        /// </summary>
        [StringLength(100)]
        [Comment("位置")]
        public virtual string PositionName { get; set; }

        /// <summary>
        /// 执行机房编码
        /// </summary>
        [StringLength(20)]
        [Comment("执行机房编码")]
        public virtual string RoomCode { get; set; }

        /// <summary>
        /// 执行机房描述
        /// </summary>
        [StringLength(50)]
        [Comment("执行机房描述")]
        public virtual string RoomName { get; set; }

        /// <summary>
        /// 附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用
        /// </summary>
        [StringLength(10)]
        [Comment("附加卡片类型 12.TCT细胞学检查申请单 11.病理检验申请单 16.门诊大型设备检查治疗项目审核、报告单（需两联）、需配合医保患者使用")]
        public virtual string AddCard { get; set; }
        /// <summary>
        /// 指引ID 关联 ExamNote表code
        /// </summary>
        [StringLength(50)]
        [Comment("指引Id 关联 ExamNote表code")]
        public string GuideCode { get; set; }

        /// <summary>
        /// 指引名称 关联 ExamNote表code
        /// </summary>
        [StringLength(2000)]
        [Comment("指引名称 关联 ExamNote表name")]
        public string GuideName { get; set; }

        /// <summary>
        /// 检查单单名标题
        /// </summary>
        [Comment("检查单单名标题")]
        public string ExamTitle { get; set; }

        /// <summary>
        /// 预约地点
        /// </summary>
        [Comment("预约地点")]
        public string ReservationPlace { get; set; }

        /// <summary>
        /// 打印模板Id
        /// </summary>
        [Comment("打印模板Id")]
        public string TemplateId { get; set; }

        /// <summary>
        /// 医嘱主要内容
        /// </summary>
        public virtual RecipeProject RecipeProject { get; set; }

        #endregion
    }
}
