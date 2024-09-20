using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.ObjectMapping;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Characters.Dto;

namespace YiJian.EMR.Characters
{
    /// <summary>
    /// 通用字符
    /// </summary>
    public interface IUniversalCharacterAppService : IApplicationService
    {
        /// <summary>
        /// 获取所有的常用符
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<List<UniversalCharacterSampleDto>>> GetAllAsync();

        /// <summary>
        /// 获取所有通用字符分类，不包含内容
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<List<UniversalCharacterDto>>> GetCategoriesAsync();

        /// <summary>
        /// 获取指定目录下的所有字符内容
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<UniversalCharacterSampleDto>> GetNodesByIdAsync(int id);

        /// <summary>
        /// 创建分类
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> CreateAsync(UniversalCharacterDto model);

        /// <summary>
        /// 更新分类内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> UpdateAsync(UniversalCharacterDto model);

        /// <summary>
        /// 移除分类内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> RemoveAsync(int id);

        /// <summary>
        /// 添加通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public Task<ResponseBase<int>> CreateNodeAsync(UniversalCharacterNodeDto model);

        /// <summary>
        /// 更新通用符内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> UpdateNodeAsync(UniversalCharacterNodeDto model);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<int>> RemoveNodeAsync(int id);


    }
}
