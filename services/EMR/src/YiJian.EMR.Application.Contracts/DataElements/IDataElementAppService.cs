using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using YiJian.EMR.DataElements;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.EMR.DataElements.Dto;

namespace YiJian.EMR.DataElements
{
    /// <summary>
    /// 数据元
    /// </summary>
    public interface IDataElementAppService : IApplicationService
    {
        /// <summary>
        /// 数据元目录树
        /// </summary>
        /// <returns></returns> 
        public Task<ResponseBase<List<DataElementTreeDto>>> GetTreeAsync();

        /// <summary>
        /// 获取数据源集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<List<DataElementRowDto>>> GetElementsAsync(Guid id);

        /// <summary>
        /// 获取数据元项详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Task<ResponseBase<DataElementItemDto>> GetDataElementItemAsync(Guid id);

        /// <summary>
        /// 更新数据元项内容
        /// </summary>
        /// <see cref="ModifyElementItemDto"/>
        /// <returns></returns> 
        public Task<ResponseBase<Guid>> ModifyElementItemAsync(ModifyElementItemDto model);

    }
}
