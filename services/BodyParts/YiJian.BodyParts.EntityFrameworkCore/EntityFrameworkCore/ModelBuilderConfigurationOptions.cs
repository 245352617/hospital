using JetBrains.Annotations;
using Volo.Abp.EntityFrameworkCore.Modeling;

namespace YiJian.BodyParts.EntityFrameworkCore
{
    public class ModelBuilderConfigurationOptions : AbpModelBuilderConfigurationOptions
    {
        public ModelBuilderConfigurationOptions()
    : base(DbProperties.TablePrefix, DbProperties.Schema)
        {

        }
    }
}