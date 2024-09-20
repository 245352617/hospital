using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace YiJian.ECIS.Core
{
    public interface IRepository : IDisposable
    {
        Task<int> ExecuteCommandAsync(string rawSql, params object[] pars);

        int ExecuteCommand(string rawSql, params object[] pars);

        DbContext DbContext { get; }
        int SaveChanges();
        Task<int> SaveChangesAsync();

    }
}
