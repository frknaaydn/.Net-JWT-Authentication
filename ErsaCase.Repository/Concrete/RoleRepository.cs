using ErsaCase.Core.Model;
using ErsaCase.Core.Repository.Concrete;
using ErsaCase.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Repository.Concrete
{
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(DbContext context) : base(context)
        {
        }
    }
}
