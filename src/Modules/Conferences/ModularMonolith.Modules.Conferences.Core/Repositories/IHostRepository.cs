using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModularMonolith.Modules.Conferences.Core.Entities;

namespace ModularMonolith.Modules.Conferences.Core.Repositories
{
    public interface IHostRepository
    {
        Task<Host> GetAsync(Guid id);
        Task<IReadOnlyList<Host>> BrowseAsync();
        Task AddAsync(Host host);
        Task UpdateAsync(Host host);
        Task DeleteAsync(Host host);
    }
}