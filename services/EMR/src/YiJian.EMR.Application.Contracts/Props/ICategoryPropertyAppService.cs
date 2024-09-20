using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services; 
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.Props.Dto;

namespace YiJian.EMR.Props
{
    /// <summary>
    /// 电子病历属性
    /// </summary>
    public interface ICategoryPropertyAppService : IApplicationService
    {
        /// <summary>
        /// 电子病历自定义属性树结构
        /// </summary>
        /// <returns></returns>
        public Task<ResponseBase<List<CategoryPropertyTreeDto>>> GetTreeAsync();

        /// <summary>
        /// 添加一个电子病历属性
        /// </summary>
        /// <see cref="CategoryPropertyDto"/>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> AddPropertyAsync(CategoryPropertyDto model);

        /// <summary>
        /// 更新一个电子病历属性
        /// </summary>
        /// <see cref="UpdateEmrPropertyDto"/>
        /// <returns></returns>
        public Task<ResponseBase<Guid>> UpdatePropertyAsync(UpdateEmrPropertyDto model);


        /// <summary>
        /// 删除指定的属性记录
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        public Task<ResponseBase<bool>> RemoveAsync(Guid id);

    }
}
