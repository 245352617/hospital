using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.Handover.EntityFrameworkCore
{
    using YiJian.Handover;
    [ConnectionStringName(HandoverDbProperties.ConnectionStringName)]
    public class HandoverDbContext : AbpDbContext<HandoverDbContext>, IHandoverDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //交接班设置表
        public DbSet<ShiftHandoverSetting> ShiftHandoverSettings { get; set; }

        //医生交接班
        public DbSet<DoctorHandover> DoctorHandovers { get; set; }
        public DbSet<DoctorPatientStatistic> DoctorPatientStatistics { get; set; }
        public DbSet<DoctorPatients> DoctorPatients { get; set; }

        
        //交班日期
        public DbSet<NurseHandover> NurseHandovers { get; set; }
        public HandoverDbContext(DbContextOptions<HandoverDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ConfigureHandover();
        }
    }
}
