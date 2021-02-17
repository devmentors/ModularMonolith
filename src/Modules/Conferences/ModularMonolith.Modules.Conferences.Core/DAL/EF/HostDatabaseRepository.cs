using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ModularMonolith.Modules.Conferences.Core.Entities;
using ModularMonolith.Modules.Conferences.Core.Repositories;

namespace ModularMonolith.Modules.Conferences.Core.DAL.EF
{
    internal class HostDatabaseRepository : IHostRepository
    {
        private readonly ConferencesDbContext _context;
        private readonly DbSet<Host> _hosts;

        public HostDatabaseRepository(ConferencesDbContext context)
        {
            _context = context;
            _hosts = _context.Hosts;
        }

        public Task<Host> GetAsync(Guid id) => _hosts.SingleOrDefaultAsync(x => x.Id == id);

        public async Task<IReadOnlyList<Host>> BrowseAsync() => await _hosts.ToListAsync();

        public async Task AddAsync(Host host)
        {
            await _hosts.AddAsync(host);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Host host)
        {
            _hosts.Update(host);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Host host)
        {
            _hosts.Remove(host);
            await _context.SaveChangesAsync();
        }
    }
}