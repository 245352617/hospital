namespace YiJian.Job.BackgroundService.Contract
{
    public interface IPatientStatusFromHisService
    {
        /// <summary>
        /// 同步His状态
        /// </summary>
        public void Publish();
    }
}
