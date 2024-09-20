using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Exceptions;
using YiJian.Recipes;

namespace YiJian.Recipe
{
    /// <summary>
    /// 新冠检测申请单
    /// </summary>
    [Authorize]
    public class NovelCoronavirusRnaAppService : RecipeAppService, INovelCoronavirusRnaAppService
    {
        private readonly INovelCoronavirusRnaRepository _novelCoronavirusRnaRepository;

        /// <summary>
        /// 新冠检测申请单
        /// </summary>
        /// <param name="novelCoronavirusRnaRepository"></param>
        public NovelCoronavirusRnaAppService(INovelCoronavirusRnaRepository novelCoronavirusRnaRepository)
        {
            _novelCoronavirusRnaRepository = novelCoronavirusRnaRepository;
        }

        /// <summary>
        /// 保存新冠rna检测申请
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        /// <exception cref="EcisBusinessException"></exception>
        public async Task<NovelCoronavirusRna> SaveAsync(NovelCoronavirusRnaCreate dto)
        {
            var model = await _novelCoronavirusRnaRepository.FirstOrDefaultAsync(f =>
                f.DoctorsAdviceId == dto.DoctorsAdviceId && f.PIID == dto.PIID);
            if (model == null)
            {
                model = await _novelCoronavirusRnaRepository.InsertAsync(new NovelCoronavirusRna(GuidGenerator.Create(),
                    dto.PIID, dto.DoctorsAdviceId,
                    dto.SpecimenType, dto.ConsultationOpinions, dto.EpidemicHistory, dto.IsFever,
                    dto.IsPneumonia, dto.IsLymphopenia, dto.PatientSource, dto.PatientIdentity, dto.PlaceToShenzhen, dto.LisesName, dto.ApplyTime));
            }
            else
            {
                model.Modify(dto.SpecimenType, dto.ConsultationOpinions, dto.EpidemicHistory, dto.IsFever,
                    dto.IsPneumonia, dto.IsLymphopenia, dto.PatientSource, dto.PatientIdentity, dto.PlaceToShenzhen, dto.LisesName, dto.ApplyTime);
                await _novelCoronavirusRnaRepository.UpdateAsync(model);
            }
            return model;
        }

        /// <summary>
        /// 获取新冠检测申请单
        /// </summary>
        /// <param name="doctorsAdviceId"></param>
        /// <param name="piid"></param>
        /// <returns></returns>
        public async Task<NovelCoronavirusRnaDto> GetAsync(Guid doctorsAdviceId, Guid piid)
        {
            var model = await _novelCoronavirusRnaRepository.FirstOrDefaultAsync(f =>
                f.DoctorsAdviceId == doctorsAdviceId && f.PIID == piid);
            return ObjectMapper.Map<NovelCoronavirusRna, NovelCoronavirusRnaDto>(model);
        }
    }
}