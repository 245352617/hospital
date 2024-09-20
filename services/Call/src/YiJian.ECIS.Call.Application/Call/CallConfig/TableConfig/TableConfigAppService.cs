using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YiJian.ECIS.Call.CallConfig.Dtos;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.CallConfig.TableConfig
{
    /// <summary>
    /// 列表配置
    /// </summary>
    public class TableConfigAppService : CallAppService, ITableConfigAppService
    {
        private readonly IRowConfigRepository _rowConfigRepository;

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="rowConfigRepository"></param>
        public TableConfigAppService(IRowConfigRepository rowConfigRepository)
        {
            _rowConfigRepository = rowConfigRepository;
        }

        /// <summary>
        /// 获取列表配置
        /// </summary>
        /// <returns></returns>
        [HttpGet("/api/call/table-config/row-configs")]
        public async Task<IEnumerable<RowConfigDto>> GetRowConfigsAsync()
        {
            var rowConfigs = await (await this._rowConfigRepository.GetQueryableAsync()).OrderBy(x => x.Order).ToListAsync();

            return ObjectMapper.Map<List<RowConfig>, List<RowConfigDto>>(rowConfigs);
        }

        /// <summary>
        /// 重置列表配置
        /// </summary>
        /// <returns></returns>
        [HttpPut("/api/call/table-config/row-configs/reset")]
        public async Task ResetAsync()
        {
            var rowConfigs = await (await this._rowConfigRepository.GetQueryableAsync()).OrderBy(x => x.Order).ToListAsync();
            rowConfigs.ForEach(x => x.Reset());
            await this._rowConfigRepository.UpdateManyAsync(rowConfigs);
        }

        /// <summary>
        /// 修改保存列表配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPut("/api/call/table-config/row-configs/save")]
        public async Task SaveRowConfigsAsync(RowConfigUpdate input)
        {
            var rowConfigs = await (await this._rowConfigRepository.GetQueryableAsync()).OrderBy(x => x.Order).ToListAsync();
            foreach (var item in input)
            {
                var editingRowConfig = rowConfigs.Find(x => x.Key == item.Key);
                if (editingRowConfig is null) Oh.Error($"列配置不存在: {item.Key}");
                editingRowConfig.Modify(item.Order, item.Visible, item.Wrap, item.Text, item.Width);
            }

            await this._rowConfigRepository.UpdateManyAsync(rowConfigs);
        }
    }
}
