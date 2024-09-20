using YiJian.ECIS.ShareModel.Enums;

namespace YiJian.ECIS.ShareModel.Etos.DoctorsAdvices;

/// <summary>
/// HIS状态更新
/// </summary> 
public class DoctorsAdviceHisStatusEto
{
    /// <summary>
    /// 医嘱ID集合
    /// </summary>
    public Guid RecipeId { get; set; }

    /// <summary>
    /// 医生工作站医嘱状态：0=未提交、1=已提交、2=已确认、3=已作废、4=已停止、6=已驳回、7=已执行
    /// </summary>
    public EDoctorsAdviceStatus Status { get; set; }

    /// <summary>
    /// 支付状态 , 0=未支付,1=已支付,2=部分支付,3=已退费
    /// </summary>
    public EPayStatus PayStatus { get; set; }
}



