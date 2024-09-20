using YiJian.BodyParts.Dtos;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Guids;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:签名
    /// </summary>
    public class IcuSignatureRepository : BaseRepository<EntityFrameworkCore.DbContext, IcuSignature, Guid>, IIcuSignatureRepository
    {
        private readonly IGuidGenerator _guidGenerator;

        public IcuSignatureRepository(IDbContextProvider<EntityFrameworkCore.DbContext> dbContextProvider,
            IGuidGenerator guidGenerator) : base(dbContextProvider)
        {
            _guidGenerator = guidGenerator;
        }


        /// <summary>
        /// 获取签名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Signature GetSignature(Guid Id)
        {
            try
            {
                Signature signature = new Signature();
                var icuSignature = DbContext.IcuSignature.Where(x => x.Id == Id).FirstOrDefault();
                if (icuSignature != null)
                {
                    signature.SignatureId = icuSignature.Id;
                    signature.SignNurseCode = icuSignature.SignNurseCode;
                    signature.SignNurseName = icuSignature.SignNurseName;
                    signature.SignImage = icuSignature.SignImage;
                }
                return signature;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// 获取签名Id,编辑签名
        /// </summary>
        /// <param name="signature"></param>
        /// <returns></returns>
        public async Task<Guid> GetSignatureId(Signature signature)
        {
            try
            {
                Guid Id = signature.SignatureId;
                var icuSignature = await DbContext.IcuSignature.Where(x => x.Id == Id).FirstOrDefaultAsync();

                if (icuSignature == null)
                {
                    icuSignature = new IcuSignature();
                }

                icuSignature.SignNurseCode = signature.SignNurseCode;
                icuSignature.SignNurseName = signature.SignNurseName;
                icuSignature.SignImage = signature.SignImage;
                if (icuSignature.Id == Guid.Empty)
                {
                    Id = Guid.NewGuid();
                    icuSignature.SignTime = DateTime.Now;
                    icuSignature.SetId(Id);
                    await DbContext.IcuSignature.AddAsync(icuSignature);
                }
                else
                {
                    await this.UpdateAsync(icuSignature);
                }

                return Id;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Guid?> AddSignatureAsync(string PI_ID, string signNurseCode, string signNurseName, DateTime? signTime, string signImage)
        {
            if (string.IsNullOrEmpty(signImage))
                return null;

            var signature = new IcuSignature(_guidGenerator.Create())
            {
                PI_ID = PI_ID,
                SignNurseCode = signNurseCode,
                SignNurseName = signNurseName,
                SignTime = signTime,
                SignImage = signImage,
                IsDeleted = false
            };
            await DbContext.IcuSignature.AddAsync(signature);
            return signature.Id;
        }

        public async Task UpdateSignatureAsync(string signatureId, string PI_ID, string signNurseCode, string signNurseName, DateTime? signTime, string signImage)
        {
            var tmpId = Guid.Parse(signatureId);
            var signatureObj = await DbContext.IcuSignature.Where(p => p.Id == tmpId).FirstOrDefaultAsync();
            if (signatureObj != null)
            {
                signatureObj.SignNurseCode = signNurseCode;
                signatureObj.SignNurseName = signNurseName;
                signatureObj.SignTime = signTime;
                signatureObj.SignImage = signImage;
                DbContext.IcuSignature.Update(signatureObj);
            }
        }

        public async Task<string> GetSignImageById(string signatureId)
        {
            if (string.IsNullOrEmpty(signatureId))
                return null;
            var tmpId = Guid.Parse(signatureId);
            return await DbContext.IcuSignature.Where(p => p.Id == tmpId).Select(s => s.SignImage).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 删除签名
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> Remove(Guid Id)
        {
            try
            {
                var signature = await DbContext.IcuSignature.AsNoTracking().Where(x => x.Id == Id).FirstOrDefaultAsync();
                if (signature != null)
                {
                    DbContext.IcuSignature.Remove(signature);
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
