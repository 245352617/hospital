using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;

namespace YiJian.BodyParts.IRepository
{
    /// <summary>
    /// 表:签名
    /// </summary>
    public interface IIcuSignatureRepository : IRepository<IcuSignature, Guid>, IBaseRepository<IcuSignature, Guid>
    {
        #region 定义接口

        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Signature GetSignature(Guid Id);

        /// <summary>
        /// 添加签名
        /// </summary>
        /// <param name="PI_ID"></param>
        /// <param name="signNurseCode"></param>
        /// <param name="signNurseName"></param>
        /// <param name="signTime"></param>
        /// <param name="signImage"></param>
        /// <returns></returns>
        Task<Guid?> AddSignatureAsync(string PI_ID, string signNurseCode, string signNurseName, DateTime? signTime, string signImage);

        /// <summary>
        /// 通过id更新签名
        /// </summary>
        /// <param name="signatureId"></param>
        /// <param name="signImage"></param>
        /// <returns></returns>
        Task UpdateSignatureAsync(string signatureId,string PI_ID, string signNurseCode, string signNurseName, DateTime? signTime, string signImage);

        /// <summary>
        /// 通过signId获取对应图片
        /// </summary>
        /// <param name="signatureId"></param>
        /// <returns></returns>
        Task<string> GetSignImageById(string signatureId);

        /// <summary>
        /// 获取签名Id,编辑签名
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        Task<Guid> GetSignatureId(Signature signature);

        /// <summary>
        /// 删除签名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        Task<bool> Remove(Guid Id);
        #endregion
    }
}
