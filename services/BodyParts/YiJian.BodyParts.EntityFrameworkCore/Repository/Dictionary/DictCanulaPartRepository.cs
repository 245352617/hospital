using YiJian.BodyParts.EntityFrameworkCore;
using YiJian.BodyParts.IRepository;
using YiJian.BodyParts.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.EntityFrameworkCore;

namespace YiJian.BodyParts.Repository
{
    /// <summary>
    /// 表:人体图-编号字典
    /// </summary>
    public class DictCanulaPartRepository : BaseRepository<YiJian.BodyParts.EntityFrameworkCore.DbContext, DictCanulaPart, Guid>, IDictCanulaPartRepository
    {
        public DictCanulaPartRepository(IDbContextProvider<YiJian.BodyParts.EntityFrameworkCore.DbContext> dbContextProvider) : base(dbContextProvider)
        {

        }


        #region 实现接口
        public async Task<bool> CheckExists(string deptCode, string moduleCode, string partName, string partNumber) {
            return await base.DbContext.DictCanulaPart.FirstOrDefaultAsync(f => f.DeptCode == deptCode && f.ModuleCode == moduleCode && f.PartName == partName && f.PartNumber == partNumber&&!f.IsDeleted) != null;
        }

        #endregion
    }
}
