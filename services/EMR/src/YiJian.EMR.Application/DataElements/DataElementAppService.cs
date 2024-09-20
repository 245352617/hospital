using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Services;
using System.Linq;
using System.Linq.Dynamic.Core;
using YiJian.EMR.DataElements;
using YiJian.ECIS.ShareModel.Responses;
using YiJian.ECIS.ShareModel.Enums;
using Volo.Abp.Uow;
using Volo.Abp;
using Microsoft.AspNetCore.Authorization;
using YiJian.EMR.DataElements.Entities;
using YiJian.EMR.DataElements.Dto;

namespace YiJian.EMR.DataElements
{
    /// <summary>
    /// 数据元
    /// </summary>
    [Authorize]
    public class DataElementAppService : EMRAppService, IDataElementAppService
    {
        /// <summary>
        /// 数据元目录树
        /// </summary>
        /// <returns></returns> 
        public async Task<ResponseBase<List<DataElementTreeDto>>> GetTreeAsync()
        { 
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取当前目录下数据元集合
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<List<DataElementRowDto>>> GetElementsAsync(Guid id)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 获取数据元项详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<DataElementItemDto>> GetDataElementItemAsync(Guid id)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

        /// <summary>
        /// 更新数据元项内容
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns> 
        public async Task<ResponseBase<Guid>> ModifyElementItemAsync(ModifyElementItemDto model)
        {
            await Task.CompletedTask;
            throw new NotImplementedException();
        }

    }
}
