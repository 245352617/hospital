namespace YiJian.Job.BackgroundService.Contract;

public interface IRecipeSplitJobService
{
    /// <summary>
    /// 长嘱定时拆分
    /// </summary>
    public void Publish();
}
