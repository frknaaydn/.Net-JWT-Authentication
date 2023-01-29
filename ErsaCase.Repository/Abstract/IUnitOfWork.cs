using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Repository.Abstract
{
    public interface IUnitOfWork
    {
        IUserRepository User { get; }
        IRoleRepository Role { get; }
        Task<int> SaveChanges();
        IList<EntityEntry> ChangeTrackerEntries();
        Task Dispose();
    }
}
