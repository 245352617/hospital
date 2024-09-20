using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace YiJian.BodyParts.IService
{
    public interface IDatabaseService : IDataSeedContributor, ITransientDependency
    {
    }
}
