using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus;
using YiJian.ECIS.Call.Domain.CallConfig;

namespace YiJian.ECIS.Call.CallConfig
{
    /// <summary>
    /// 创建部门时，添加默认排队号规则
    /// </summary>
    public class DepartmentCreatedEventHandler : ILocalEventHandler<DepartmentCreatedEvent>, ITransientDependency
    {
        private readonly ISerialNoRuleRepository _serialNoRuleRepository;

        public DepartmentCreatedEventHandler(ISerialNoRuleRepository serialNoRuleRepository)
        {
            this._serialNoRuleRepository = serialNoRuleRepository;
        }

        public async Task HandleEventAsync(DepartmentCreatedEvent eventData)
        {
            // 默认没有前缀，且长度为4位
            var serialNoRule = new SerialNoRule(eventData.Id, "", 4);
            var record = await this._serialNoRuleRepository.InsertAsync(serialNoRule);
        }
    }
}
