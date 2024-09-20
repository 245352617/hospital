using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using YiJian.ECIS.Call;
using YiJian.ECIS.Call.Domain;
using YiJian.ECIS.Call.Domain.CallConfig;
using YiJian.ECIS.Call.Domain.CallCenter;
using YiJian.ECIS.Call.CallConfig;

namespace YiJian.ECIS.Call.EntityFrameworkCore
{
    [ConnectionStringName(CallDbProperties.ConnectionStringName)]
    public class CallDbContext : AbpDbContext<CallDbContext>, ICallDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        public DbSet<Department> Departments { get; set; }
        public DbSet<ConsultingRoom> ConsultingRooms { get; set; }

        /// <summary>
        /// 诊室固定
        /// </summary>
        public DbSet<ConsultingRoomRegular> ConsultingRoomRegulars { get; set; }

        /// <summary>
        /// 医生变动
        /// </summary>
        public DbSet<DoctorRegular> DoctorRegulars { get; set; }

        /// <summary>
        /// 基础设置
        /// </summary>
        public DbSet<BaseConfig> BaseConfigs { get; set; }

        /// <summary>
        /// 排队号规则
        /// </summary>
        public DbSet<SerialNoRule> SerialNoRules { get; set; }

        /// <summary>
        /// 叫号患者列表
        /// </summary>
        public DbSet<CallInfo> CallInfos { get; set; }

        /// <summary>
        /// 列配置
        /// </summary>
        public DbSet<RowConfig> RowConfigs { get; set; }

        public CallDbContext(DbContextOptions<CallDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuidler)
        {
            base.OnModelCreating(modelBuidler);

            modelBuidler.ConfigureCall();
        }
    }
}