using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Volo.Abp.Domain.Entities.Auditing;
using YiJian.AuditLogs.Enums;

namespace YiJian.AuditLogs.Entities
{
    /// <summary>
    /// HIS接口审计日志
    /// </summary>
    [Comment("HIS接口审计日志")]
    public class AuditLog : CreationAuditedEntity<int>
    {
        /// <summary>
        /// HIS接口审计日志
        /// </summary>
        private AuditLog()
        {

        }

        /// <summary>
        /// HIS接口审计日志
        /// </summary>
        /// <param name="methodName">方法名称</param>
        /// <param name="httpMethods">方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6</param>
        /// <param name="url">请求的HTTP URL</param>
        /// <param name="visSerialNo">流水号</param>
        /// <param name="patientId">患者id</param>
        /// <param name="request">请求参数</param>
        /// <param name="response">返回数据记录</param>
        /// <param name="operationType">操作类型，0=请求，1=返回</param>
        public AuditLog(string methodName, EMethod httpMethods, string url, string visSerialNo, string patientId, string request, string response, int operationType = 0)
        {
            MethodName = methodName;
            HttpMethods = httpMethods;
            Url = url;
            VisSerialNo = visSerialNo;
            PatientId = patientId;
            Request = request;
            Response = response;
            OperationType = operationType;
        }

        /// <summary>
        /// 方法名称
        /// </summary>
        [Comment("方法名称")]
        [StringLength(100)]
        public string MethodName { get; set; }

        /// <summary>
        /// 方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6
        /// </summary>
        [Comment("方法类型  GET = 0, POST = 1, PUT = 2,DELETE = 3,HEAD = 4,OPTIONS = 5, PATCH = 6")]
        public EMethod HttpMethods { get; set; }

        /// <summary>
        /// 请求的HTTP URL
        /// </summary>
        [Comment("请求的HTTP URL")]
        [StringLength(200)]
        public string Url { get; set; }

        /// <summary>
        /// 就诊流水号
        /// </summary>
        [JsonProperty("visSerialNo")]
        [StringLength(64)]
        public string VisSerialNo { get; set; }

        /// <summary>
        /// 病人ID
        /// </summary>
        [JsonProperty("patientId")]
        [StringLength(32)]
        public string PatientId { get; set; }

        /// <summary>
        /// 请求参数（入参）
        /// </summary>
        [Comment("请求参数")]
        [Column(TypeName = "ntext")]
        public string Request { get; set; }

        /// <summary>
        /// 返回数据记录
        /// </summary>
        [Comment("返回数据")]
        [Column(TypeName = "ntext")]
        public string Response { get; set; }

        /// <summary>
        /// 操作类型，0=请求，1=返回
        /// </summary>
        [Comment(" 操作类型，0=请求，1=返回")]
        public int OperationType { get; set; }
    }

}
