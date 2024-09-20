namespace YiJian.Job.BackgroundService.Contract;

public interface ISynctoxicJobService
{ 
    /// <summary>
    /// 检查患者信息是否存在的作业
    /// </summary>
    public void Publish();
}
