using ErsaCase.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ErsaCase.Repository.Context
{
    public class ErsaDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles{ get; set; }


        public ErsaDbContext(DbContextOptions<ErsaDbContext> options) : base(options)
        {
        }
    }
}
