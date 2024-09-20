using Volo.Abp.Modularity;
using Yijian.ECIS.RecipeSplit;
using Yijian.ECIS.RecipeSplit.Contracts;

namespace YiJian.Nursing
{
    [DependsOn(
       typeof(RecipeSplitContractModule),
        typeof(RecipeSplitModule)
        )]
    public class RecipeSplitTestsModule : AbpModule
    {

    }
}
