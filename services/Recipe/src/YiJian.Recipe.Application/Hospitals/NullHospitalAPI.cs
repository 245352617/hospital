using YiJian.Documents.Dto;

namespace YiJian.Hospitals
{
    public class NullHospitalAPI : IHospitalAPI
    {
        /// <summary>
        /// 空白实现
        /// </summary>
        /// <returns></returns>
        public DocumentResponseDto SetCodeUrl(DocumentResponseDto data)
        {
            return data;
        }
    }
}
