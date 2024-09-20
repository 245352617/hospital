using System.ComponentModel;

namespace YiJian.Documents.Enums
{
    /// <summary>
    /// 命令参数(0=获取处方单,1=获取注射单,2=获取输液单,3=获取检验单,4=获取检查单,5=获取处置单,6=获取治疗单,7=获取物化单,8=获取预防接种单)
    /// </summary>
    public enum ECommandParam : int
    {
        /// <summary>
        /// 获取处方单
        /// </summary>
        [Description("获取处方单")]
        GetMedicine = 0,

        /// <summary>
        /// 获取注射单
        /// </summary>
        [Description("获取注射单")]
        GetInjection = 1,

        /// <summary>
        /// 获取输液单
        /// </summary>
        [Description("获取输液单")]
        GetTransfusion = 2,


        /// <summary>
        /// 获取检验单
        /// </summary>
        [Description("获取检验单")]
        GetLaboratory = 3,


        /// <summary>
        /// 获取检查单
        /// </summary>
        [Description("获取检查单")]
        GetExamine = 4,

        /// <summary>
        /// 获取处置单
        /// </summary>
        [Description("获取处置单")]
        GetDisposal = 5,

        /// <summary>
        /// 获取诊疗单
        /// </summary>
        [Description("获取诊疗单")]
        GetTreat = 6,

        /// <summary>
        /// 获取物化单
        /// </summary>
        [Description("获取雾化单")]
        GetAerosolization = 7,


        /// <summary>
        /// 获取预防接种单
        /// </summary>
        [Description("获取预防接种单")]
        GetPremunitive = 8,

        /// <summary>
        /// 麻药 精一单
        /// </summary>
        [Description("麻药 精一单")]
        GetAnesthetic = 9,
        /// <summary>
        /// 精二单
        /// </summary>
        [Description("精二单")]
        GetPsychotropicDrugsII = 10,




    }
}
