using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModularMonolith.Modules.Speakers.Core.Entities;

namespace ModularMonolith.Modules.Speakers.Core.DAL.Repositories
{
    public interface ISpeakersRepository
    {
        Task<IReadOnlyList<Speaker>> BrowseAsync();
        Task<Speaker> GetAsync(Guid id);
        Task<bool> ExistsAsync(Guid id);
        Task AddAsync(Speaker speaker);
        Task UpdateAsync(Speaker speaker);
    }
}