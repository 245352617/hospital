using YiJian.ECIS.ShareModel.RpcContract.Patient.Model;

namespace YiJian.ECIS.ShareModel.RpcContract.Patient
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPatientServer
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<HelloReply> SayHelloAsync(HelloRequest request);
    }
}
