using System;

namespace SamJan.MicroService.PreHospital.TriageService
{
    public class ResetVitalSignExpressionDto
    {
        public Guid Id { get; set; }

        /// <summary>
        /// 级别：Ⅰ级:StLevelExpression,Ⅱ级:NdLevelExpression,III级:RdExpression,Ⅳa级:ThaExpression,Ⅳb级:ThbExpression
        /// </summary>
        public string Level { get; set; }
    }
}