using YiJian.Documents.Dto;

namespace YiJian.Hospitals
{
    public interface IHospitalAPI
    {
        /// <summary>
        /// 获得二维码地址
        /// </summary>
        /// <returns></returns>
        public DocumentResponseDto SetCodeUrl(DocumentResponseDto data);
    }
}
