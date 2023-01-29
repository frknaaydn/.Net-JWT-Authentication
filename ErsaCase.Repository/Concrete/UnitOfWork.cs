using ErsaCase.Repository.Abstract;
using ErsaCase.Repository.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ErsaCase.Repository.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {

        public readonly UserRepository _userRepository;
        public readonly RoleRepository _roleRepository;

        public readonly ErsaDbContext _context;


        public UnitOfWork(ErsaDbContext context)
        {
            _context = context;
        }

        public IUserRepository User => _userRepository ?? new UserRepository(_context);
        public IRoleRepository Role => _roleRepository ?? new RoleRepository(_context);
        public async Task<int> SaveChanges()
        {
            var aaa = _context.ChangeTracker.Entries();
            return await _context.SaveChangesAsync();
        }

        public IList<EntityEntry> ChangeTrackerEntries()
        {
            var aaa = _context.ChangeTracker.Entries().ToList();

            return aaa;
        }

        public async Task Dispose()
        {
            await _context.DisposeAsync();

            //return await _context.;
        }

    }
}
