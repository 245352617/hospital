using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.MasterData.Medicines
{
    /// <summary>
    /// 描    述 ：护士医嘱类别配置服务
    /// 创 建 人 ：杨凯
    /// 创建时间 ：2023/8/24 16:05:06
    /// </summary>
    [Authorize]
    public class NursingRecipeTypeAppService : MasterDataAppService
    {
        private readonly INursingRecipeTypeRepository _nursingRecipeTypeRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="nursingRecipeTypeRepository"></param>
        public NursingRecipeTypeAppService(INursingRecipeTypeRepository nursingRecipeTypeRepository)
        {
            _nursingRecipeTypeRepository = nursingRecipeTypeRepository;
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="nursingRecipeTypes"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> SaveAsync(List<NursingRecipeType> nursingRecipeTypes)
        {
            await _nursingRecipeTypeRepository.DeleteAsync(x => true);

            foreach (NursingRecipeType item in nursingRecipeTypes)
            {
                item.SetId(Guid.NewGuid());
            }

            if (nursingRecipeTypes.Any()) await _nursingRecipeTypeRepository.InsertManyAsync(nursingRecipeTypes);

            return true;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<NursingRecipeType>> GetListAsync()
        {
            return await _nursingRecipeTypeRepository.GetListAsync();
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> DeleteAsync(string typeName)
        {
            if (string.IsNullOrEmpty(typeName)) Oh.Error("请求参数为空"); ;

            await _nursingRecipeTypeRepository.DeleteAsync(x => x.TypeName == typeName);

            return true;
        }
    }
}
