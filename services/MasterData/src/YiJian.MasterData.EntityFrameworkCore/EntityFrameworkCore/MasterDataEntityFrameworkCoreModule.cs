using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using YiJian.ECIS.Core;
using YiJian.MasterData.Domain;

namespace YiJian.MasterData.EntityFrameworkCore;

[DependsOn(
    typeof(MasterDataDomainModule),
    typeof(AbpEntityFrameworkCoreModule)
)]
public class MasterDataEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddAbpDbContext<MasterDataDbContext>(options =>
        {
            /* Add custom repositories here. Example:
             * options.AddRepository<Question, EfCoreQuestionRepository>();
             * 
             */
        });
        //因为用的是原生的没有默认注入就手动注入
        //context.Services.AddScoped<ITreeRepository<ExamTree>>(sp => sp.GetRequiredService<TreeRepository<ExamTree>>());
        context.Services.AddScoped<IExamTreeRepository, ExamTreeRepository>();
        context.Services.AddScoped<ILabTreeRepository, LabTreeRepository>();
    }
}