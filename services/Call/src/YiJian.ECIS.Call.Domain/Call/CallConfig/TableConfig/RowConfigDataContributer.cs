using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 列配置种子数据
    /// </summary>
    public class RowConfigDataContributer : IDataSeedContributor, ITransientDependency
    {
        private readonly IRowConfigRepository _rowConfigRepository;

        public RowConfigDataContributer(IRowConfigRepository rowConfigRepository)
        {
            this._rowConfigRepository = rowConfigRepository;
        }

        /// <summary>
        /// 播种
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task SeedAsync(DataSeedContext context)
        {
            List<RowConfig> defaultRowConfigs = new()
            {
                new(key: "visitStatus", order: 1, field: "visitStatus", text: "状态", width: 85),
                new(key: "logTime", order: 2, field: "logTime", text: "登记时间", width: 280),
                new(key: "callingSn", order: 3, field: "callingSn", text: "排队号", width: 120),
                new(key: "actTriageLevelName", order: 4, field: "actTriageLevelName", text: "分诊等级", width: 100),
                new(key: "patientName", order: 5, field: "patientName", text: "姓名", width: 100),
                new(key: "sexName", order: 6, field: "sexName", text: "性别", width: 140),
                new(key: "ageString", order: 7, field: "ageString", text: "年龄", width: 140),
                new(key: "contactsPhone", order: 8, field: "contactsPhone", text: "联系", width: 140),
                new(key: "registerNo", order: 9, field: "registerNo", text: "就诊ID", width: 100),
                new(key: "chargeTypeName", order: 10, field: "chargeTypeName", text: "费别", width: 0),
                new(key: "triageDeptName", order: 11, field: "triageDeptName", text: "科室", width: 160),
                new(key: "doctorName", order: 12, field: "doctorName", text: "急诊医生", width: 120),
                new(key: "consultingRoomName", order: 13, field: "consultingRoomName", text: "就诊房间", width: 120),
                new(key: "waitingDuration", order: 14, field: "waitingDuration", text: "等候时长", width: 120),
                new(key: "treatingDuration", order: 15, field: "treatingDuration", text: "就诊时长", width: 120),
            };
            var currentConfigs = await this._rowConfigRepository.GetListAsync();
            var newConfigs = defaultRowConfigs.Where(x => !currentConfigs.Exists(y => x.Key == y.Key)).ToList();

            await this._rowConfigRepository.InsertManyAsync(newConfigs);
        }
    }
}
