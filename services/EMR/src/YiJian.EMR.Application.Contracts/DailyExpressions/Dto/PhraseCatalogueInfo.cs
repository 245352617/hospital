using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Localization;
using YiJian.EMR.Enums;

namespace YiJian.EMR.DailyExpressions.Dto
{
    /// <summary>
    /// 常用语目录
    /// </summary>
    public class PhraseCatalogueInfoDto:EntityDto<int?>
    { 
        /// <summary>
        /// 目录标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 模板类型，0=通用(全院)，1=科室，2=个人
        /// </summary>
        public ETemplateType TemplateType { get; set; }

        /// <summary>
        /// 归属人 如果 TemplateType=2 归属者为医生Id doctorId, 如果 TemplateType=1 归属者为科室id deptid , 如果 TemplateType=0 归属者为"hospital"
        /// </summary>
        public string Belonger { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 期望展示的权限 -1=拒绝，没有任何权限  , 0=管理员 , 1=医生 , 2=护士
        /// </summary>
        public EExpectedRole Role { get;set; }

        /// <summary>
        /// 是否有权限操作
        /// </summary>
        /// <returns></returns>
        public bool hasPermission()
        {
            if ((int)Role > 0 && TemplateType == ETemplateType.Personal) return true;
            if (Role == EExpectedRole.Admin && (TemplateType == ETemplateType.General || TemplateType == ETemplateType.Department)) return true; 
            return false; 
        }

    }

}
