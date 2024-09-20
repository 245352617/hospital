using Microsoft.Extensions.Configuration;
using YiJian.Documents.Dto;

namespace YiJian.Hospitals
{
    /// <summary>
    /// 北大深圳医院特用API
    /// </summary>
    public class PKUHospitalAPI : IHospitalAPI
    {

        private readonly IConfiguration _configuration;

        #region 构造函数
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public PKUHospitalAPI(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        #endregion

        /// <summary>
        /// 获得二维码地址
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public DocumentResponseDto SetCodeUrl(DocumentResponseDto data)
        {
            if (data == null) return data;

            var record = data.AdmissionRecords[0];
            string baseUrl = "https://yyjf.pkuszh.com/hospapi/sl?";
            var payUrl = $"{baseUrl}mzType=smf&cardNo={record.VisitNo}&name={record.PatientName}&unitId=1";

            if (data.Medicines != null)
            {
                data.Medicines?.ForEach(f => { f.Lgjkzx_payurl = payUrl; f.Lgzxyy_payurl = payUrl; });
            }
            if (data.Pacses != null)
            {
                data.Pacses?.ForEach(f => { f.Lgjkzx_payurl = payUrl; f.Lgzxyy_payurl = payUrl; });
            }
            if (data.Treats != null)
            {
                data.Treats?.ForEach(f => { f.Lgjkzx_payurl = payUrl; f.Lgzxyy_payurl = payUrl; });
            }
            if (data.Lises != null)
            {
                data.Lises?.ForEach(f => { f.Lgjkzx_payurl = payUrl; f.Lgzxyy_payurl = payUrl; });
            }

            return data;
        }

        // /// <summary>
        // /// MD5加密
        // /// </summary>
        // /// <param name="input"></param>
        // /// <returns></returns>
        // public string ComputeMD5(string input)
        // {
        //     // 将输入字符串转换为字节数组
        //     byte[] inputBytes = Encoding.UTF8.GetBytes(input);
        //
        //     // 创建MD5对象
        //     using (MD5 md5 = MD5.Create())
        //     {
        //         // 计算哈希值
        //         byte[] hashBytes = md5.ComputeHash(inputBytes);  // 不允许使用不安全的加密算法  by: ywlin 2024-07-01
        //
        //         // 将哈希值转换为字符串
        //         StringBuilder sb = new StringBuilder();
        //         foreach (byte b in hashBytes)
        //         {
        //             sb.Append(b.ToString("x2"));
        //         }
        //         return sb.ToString();
        //     }
        // }
    }
}
