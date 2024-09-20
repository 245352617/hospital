using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using YiJian.ECIS.ShareModel.Enums;
using YiJian.ECIS.ShareModel.Responses;

namespace YiJian.Health.Report.PageSizes
{
    /// <summary>
    /// 纸张大小设置
    /// </summary>
    [Authorize]
    public class PageSizeAppService : ReportAppService, IPageSizeAppService
    {
        private readonly IPageSizeRepository _pageSizeRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSizeRepository"></param>
        public PageSizeAppService(IPageSizeRepository pageSizeRepository)
        {
            _pageSizeRepository = pageSizeRepository;
        }

        /// <summary>
        /// 保存纸张大小
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> SaveAsync(PageSizeDto dto)
        {
            if (dto.Id == Guid.Empty)
            {
                await _pageSizeRepository.InsertAsync(new PageSize(dto.Code, dto.Height, dto.Width));
                return new ResponseBase<bool>(EStatusCode.C200);
            }

            var model = await _pageSizeRepository.FirstOrDefaultAsync(x => x.Id == dto.Id);
            if (model == null)
            {
                return new ResponseBase<bool>(EStatusCode.C400, "数据不存在");
            }

            model.Update(dto.Height, dto.Width);
            await _pageSizeRepository.UpdateAsync(model);
            return new ResponseBase<bool>(EStatusCode.C200);
        }

        /// <summary>
        /// 删除纸张大小设置
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ResponseBase<bool>> DeleteAsync(Guid id)
        {
            if (!await _pageSizeRepository.AnyAsync(x => x.Id == id))
            {
                return new ResponseBase<bool>(EStatusCode.C400, "数据不存在");
            }

            await _pageSizeRepository.DeleteAsync(id);
            return new ResponseBase<bool>(EStatusCode.C200);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseBase<List<PageSizeDto>>> GetListAsync()
        {
            var pageList = await _pageSizeRepository.GetListAsync();
            return new ResponseBase<List<PageSizeDto>>(EStatusCode.C200,
                data: ObjectMapper.Map<List<PageSize>, List<PageSizeDto>>(pageList));
        }
    }
}