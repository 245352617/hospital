using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Nito.AsyncEx;
using System;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using YiJian.ECIS.Domain.Call;
using YiJian.ECIS.ShareModel.Exceptions;

namespace YiJian.ECIS.Call.Domain.CallConfig
{

    /// <summary>
    /// 【排队号规则】领域服务
    /// </summary>
    public class SerialNoRuleManager : DomainService
    {
        private readonly ISerialNoRuleRepository _serialNoRuleRepository;
        private readonly BaseConfigManager _baseConfigManager;
        private readonly IConfiguration _configuration;
        private readonly AsyncLock _nolock = new AsyncLock();


        public SerialNoRuleManager(ISerialNoRuleRepository serialNoRuleRepository,
            BaseConfigManager baseConfigManager,
            IConfiguration configuration)
        {
            this._serialNoRuleRepository = serialNoRuleRepository;
            this._baseConfigManager = baseConfigManager;
            this._configuration = configuration;
        }

        /// <summary>
        /// 删除【排队号规则】规则
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync([NotNull] int id)
        {
            var existingItem = await _serialNoRuleRepository.FirstOrDefaultAsync(p => p.Id == id);
            if (existingItem == null)
            {// 【排队号规则】规则不存在
                //throw new EcisBusinessException(CallErrorCodes.SerialNoRuleNotExists);
                throw new EcisBusinessException(message: "排队号规则不存在");
            }

            await _serialNoRuleRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 获取科室排队号（叫号）
        /// </summary>
        /// <param name="departmentId">科室id</param>
        /// <param name="triageLevel">分诊级别</param>
        /// <returns></returns>
        public async Task<string> GetSerialNoAsync(Guid departmentId, string triageLevel)
        {
            // 获取当前科室排队号规则
            var serialNoRule = await _serialNoRuleRepository.GetSerialNoRuleAsync(departmentId);
            if (serialNoRule is null)
            {// 科室未设置排队号规则
                // 创建默认排队号规则，默认没有前缀，且长度为4位
                throw new EcisBusinessException("请创建当前科室排队号规则之后再试！");
                //serialNoRule = await _serialNoRuleRepository.InsertAsync(new SerialNoRule(departmentId, "", 4));
            }
            var baseConfig = await _baseConfigManager.GetCurrentConfigAsync();
            // 获取叫号基础设置
            var refreshDateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, baseConfig.CurrentUpdateNoHour, baseConfig.CurrentUpdateNoMinute, 0);
            int.TryParse(_configuration["InitSerialNo"], out int InitSerialNo);

            using (await _nolock.LockAsync())
            //锁保护区域
            {
                if (serialNoRule.SerialDateTime < refreshDateTime)
                {// 当排队号流水号过期，则重置流水号
                    serialNoRule.ResetSerialNo(InitSerialNo);
                }
            }

            var snBuilder = new StringBuilder();
            if (!string.IsNullOrWhiteSpace(triageLevel))
            {
                int.TryParse(triageLevel.Substring(triageLevel.Length - 1, 1), out int level);
                //分诊级别大于4级的需要单独生成排队号
                if (level > 0 && level < 4)
                {
                    // 取号（下一个排队号）
                    serialNoRule.GoCriticalNext();
                    snBuilder.Append(serialNoRule.Prefix);
                    snBuilder.Append(serialNoRule.CriticalCurrentNo.ToString($"D{serialNoRule.SerialLength}"));

                }
                else
                {
                    // 取号（下一个排队号）
                    serialNoRule.GoNext();
                    snBuilder.Append(serialNoRule.Prefix);
                    snBuilder.Append(serialNoRule.CurrentNo.ToString($"D{serialNoRule.SerialLength}"));
                }
            }
            else
            {
                // 取号（下一个排队号）
                serialNoRule.GoNext();
                snBuilder.Append(serialNoRule.Prefix);
                snBuilder.Append(serialNoRule.CurrentNo.ToString($"D{serialNoRule.SerialLength}"));
            }

            await this._serialNoRuleRepository.UpdateAsync(serialNoRule);

            return snBuilder.ToString();
        }
    }
}
