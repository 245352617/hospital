using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq.Expressions;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    [ConnectionStringName(DbProperties.ConnectionStringName)]
    public class DbContext : AbpDbContext<DbContext>, IDbContext
    {
        /* Add DbSet for each Aggregate Root here. Example:
         * public DbSet<Question> Questions { get; set; }
         */
        //.........................
        public virtual DbSet<Dict> Dict { get; set; }
        //public virtual DbSet<IcuCanula> IcuCanula { get; set; }
        public virtual DbSet<IcuDeptSchedule> IcuDeptSchedule { get; set; }
        public virtual DbSet<IcuParaItem> IcuParaItem { get; set; }
        public virtual DbSet<IcuParaModule> IcuParaModule { get; set; }
        //public virtual DbSet<IcuPatientRecord> IcuPatientRecord { get; set; }
        public virtual DbSet<IcuSysPara> IcuSysPara { get; set; }
        public virtual DbSet<IcuNursingSkin> IcuNursingSkin { get; set; }
        public virtual DbSet<IcuSkin> IcuSkin { get; set; }
        public virtual DbSet<DictCanulaPart> DictCanulaPart { get; set; }
        public virtual DbSet<SkinDynamic> SkinDynamic { get; set; }

        public virtual DbSet<DictSource> DictSource { get; set; }
        public virtual DbSet<IcuNursingEvent> IcuNursingEvent { get; set; }
        public virtual DbSet<IcuSignature> IcuSignature { get; set; }
        public virtual DbSet<FileRecord> FileRecord { get; set; }
        public virtual DbSet<IcuPhrase> IcuPhrase { get; set; }

        public DbContext(DbContextOptions<DbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
         
            builder.Configure();
        }
        //public static readonly ILoggerFactory loggerFactory= LoggerFactory.Create(builder => { builder.AddConsole(); });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();

            var loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new EFLoggerProvider());
            optionsBuilder.UseLoggerFactory(loggerFactory);

            base.OnConfiguring(optionsBuilder);
            //optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}