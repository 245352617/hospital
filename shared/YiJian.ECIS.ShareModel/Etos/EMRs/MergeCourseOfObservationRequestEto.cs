namespace YiJian.ECIS.ShareModel.Etos.EMRs
{

    /// <summary>
    /// 合并电子病历的请求参数
    /// </summary>
    public class MergeCourseOfObservationRequestEto
    {
        /// <summary>
        /// 患者唯一Id
        /// </summary>
        public Guid Piid { get; set; }

        /// <summary>
        /// 电子病历的源模板
        /// </summary>
        public Guid OriginId { get; set; }
    }
}