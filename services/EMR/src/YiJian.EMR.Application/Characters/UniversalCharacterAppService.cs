using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using SkyWalking.NetworkProtocol.V3;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Characters.Contracts;
using YiJian.EMR.Characters.Dto;
using YiJian.EMR.Characters.Entities;

namespace YiJian.EMR.Characters
{
    /// <summary>
    /// 通用字符
    /// </summary>
    [Authorize]
    public class UniversalCharacterAppService : EMRAppService, IUniversalCharacterAppService
    {
        private readonly IUniversalCharacterRepository _universalCharacterRepository;
        private readonly IUniversalCharacterNodesRepository _universalCharacterNodesRepository;

        public UniversalCharacterAppService(IUniversalCharacterRepository universalCharacterRepository,
            IUniversalCharacterNodesRepository universalCharacterNodesRepository)
        {
            _universalCharacterRepository = universalCharacterRepository;
            _universalCharacterNodesRepository = universalCharacterNodesRepository;
        }

        /// <summary>
        /// 获取所有的常用符
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<UniversalCharacterSampleDto>>> GetAllAsync()
        {
            var data = await  _universalCharacterRepository.GetAllAsync();
            var map = ObjectMapper.Map<List<UniversalCharacter>, List<UniversalCharacterSampleDto>>(data);
            return new ResponseBase<List<UniversalCharacterSampleDto>>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 获取所有通用字符分类，不包含内容
        /// </summary>
        /// <returns></returns> 
        public async Task<ResponseBase<List<UniversalCharacterDto>>> GetCategoriesAsync()
        {
            var data = await _universalCharacterRepository.GetCategoriesAsync();
            var map = ObjectMapper.Map<List<UniversalCharacter>,List<UniversalCharacterDto>>(data);
            return new ResponseBase<List<UniversalCharacterDto>>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 获取指定目录下的所有字符内容
        /// </summary>
        /// <returns></returns> 
        public async Task<ResponseBase<UniversalCharacterSampleDto>> GetNodesByIdAsync(int id)
        {
            var data = await _universalCharacterRepository.GetNodesByIdAsync(id);
            var map = ObjectMapper.Map<UniversalCharacter, UniversalCharacterSampleDto>(data); 
            return new ResponseBase<UniversalCharacterSampleDto>(EStatusCode.COK, map);
        }

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> CreateAsync(UniversalCharacterDto model)
        {
            var entity = new UniversalCharacter(model.Category,model.Sort);
            var data = await _universalCharacterRepository.CreateAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK,data);
        }

        /// <summary>
        /// 更新分类内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> UpdateAsync(UniversalCharacterDto model)
        {
            var entity = await _universalCharacterRepository.FirstOrDefaultAsync(w=>w.Id== model.Id.Value);
            entity.Update(model.Category,model.Sort);
            var data = await _universalCharacterRepository.UpdateAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK, data);
        }

        /// <summary>
        /// 移除分类内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> RemoveAsync(int id)
        {
            var data = await _universalCharacterRepository.RemoveAsync(id);
            return new ResponseBase<int>(EStatusCode.COK, data);
        }
         
        /// <summary>
        /// 添加通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<int>> CreateNodeAsync(UniversalCharacterNodeDto model)
        {
            var node = new UniversalCharacterNode(model.Character,model.Sort,model.UniversalCharacterId); 
            var data = await _universalCharacterNodesRepository.CreateAsync(node);
            return new ResponseBase<int>(EStatusCode.COK,data); 
        }

        /// <summary>
        /// 更新通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> UpdateNodeAsync(UniversalCharacterNodeDto model)
        {
            var entity = await _universalCharacterNodesRepository.FirstOrDefaultAsync(w => w.Id == model.Id);
            entity.Update(model.Character,model.Sort,model.UniversalCharacterId);
            var data = await _universalCharacterNodesRepository.UpdateAsync(entity);
            return new ResponseBase<int>(EStatusCode.COK,data);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<int>> RemoveNodeAsync(int id)
        {
            var data = await _universalCharacterNodesRepository.RemoveAsync(id);
            return new ResponseBase<int>(EStatusCode.COK,data);
        }


    }
}
