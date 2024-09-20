using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 调用 HIS 接口异常
    /// </summary>
    public class HisResponseException: Exception
    {
        public HisResponseException(string message): base(message)
        {
            
        }
    }
}