namespace YiJian.ECIS.ShareModel.Models
{

    /// <summary>
    /// 配置
    /// </summary>
    public class MinioSetting
    {
        /// <summary>
        /// 
        /// </summary>
        public string Endpoint { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 存储对象
        /// </summary>
        public Bucket Bucket { get; set; }


    }

    /// <summary>
    /// 存储对象
    /// </summary>
    public class Bucket
    {
        /// <summary>
        /// 
        /// </summary>
        public string Emr { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Writer { get; set; }

        /// <summary>
        /// 电子病历合并的路径
        /// </summary>
        public string Merge { get; set; }

    }
}