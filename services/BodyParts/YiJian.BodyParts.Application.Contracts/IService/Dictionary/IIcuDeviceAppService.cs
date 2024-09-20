using YiJian.BodyParts.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace YiJian.BodyParts.IService
{
    /// <summary>
    /// 表:字典-设备表
    /// </summary>
    public interface IIcuDeviceAppService : IApplicationService
    /**ICrudAppService<
            IcuDeviceDto,
            Guid, //数据实体的主键
            PagedAndSortedResultRequestDto, //获取数据的时候用于分页和排序
            CreateUpdateIcuDeviceDto, //用于创建数据
            CreateUpdateIcuDeviceDto> //用于更新数据
    **/
    {


        Task<JsonResult<List<IcuDeviceTypeDto>>> SyncDeviceType();

        Task<JsonResult<List<IcuDeviceDto>>> SyncDevice();

        JsonResult<List<IcuDeviceTypeDto>> SelectIotDeviceTypeList();

        JsonResult<List<IcuDeviceDto>> SelectIotDeviceList(string deviceType);

        Task<JsonResult<List<IcuDeviceTypeDto>>> SelectDeviceTypeList();

        Task<JsonResult<List<BedDeviceDto>>> SelectBedDeviceList(string deptCode, string deviceType);

        Task<JsonResult<BedDeviceDto>> SelectBedDevice(string deviceCode);

        Task<JsonResult<List<IcuBedDto>>> SelectBedNullList(string deptCode, string deviceType);

        Task<JsonResult<BedDeviceDto>> CreateBedDevice(BedDeviceDto bedDeviceDto);

        /// <summary>
        /// 定时获取仪器状态
        /// </summary>
        Task<JsonResult> AutoDisposeMonitorList();

        #region 服务接口定义


        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //Task<ReturnResult<bool>> PostAsync(CreateUpdateIcuDeviceDto dto, CancellationToken cancellationToken);

        ///// <summary>
        ///// 更新
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //Task<ReturnResult<bool>> PutAsync(UpdateIcuDeviceDto dto, CancellationToken cancellationToken);

        ///// <summary>
        ///// 删除
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //Task<ReturnResult<bool>> DeleteAsync(Guid Id, CancellationToken cancellationToken);

        ///// <summary>
        ///// 详情，明细
        ///// </summary>
        ///// <param name="Id"></param>
        ///// <returns></returns>
        //Task<ReturnResult<IcuDeviceDto>> GetAsync(Guid Id, CancellationToken cancellationToken);

        ///// <summary>
        ///// 分页
        ///// </summary>
        ///// <param name="dto"></param>
        ///// <returns></returns>
        //Task<ReturnResult<PageReturnResult<List<IcuDeviceDto>>>> PageAsync(
        //    PagedAndSortedMultipleWhereResultRequest page, CancellationToken cancellationToken);



        #endregion
    }
}
