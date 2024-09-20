using System.Collections.Generic;
using YiJian.Patient;

namespace YiJian.Nursing.Recipes.Dtos
{
    /// <summary>
    /// 描    述 ：瓶贴打印Dto
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/26 15:26:19
    /// </summary>
    public class BottleLableDto
    {
        /// <summary>
        /// 患者信息
        /// </summary>
        public List<AdmissionRecordDto> admissionRecords { get; set; }

        /// <summary>
        /// 执行单信息
        /// </summary>
        public List<RecipeExecDto> recipeExecs { get; set; }
    }
}
