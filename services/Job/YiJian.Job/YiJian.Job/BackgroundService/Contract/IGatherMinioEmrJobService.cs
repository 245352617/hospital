namespace YiJian.Job.BackgroundService.Contract;

public interface IGatherMinioEmrJobService
{
    /// <summary>
    /// 采集Minio电子病历PDF信息的作业
    /// </summary>
    public void Publish();
}
