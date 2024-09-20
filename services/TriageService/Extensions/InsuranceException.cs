using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    /// <summary>
    /// 省医保异常信息
    /// </summary>
    public class InsuranceException : Exception
    {
        public InsuranceException(string message): base(message)
        {
            
        }
    }
}