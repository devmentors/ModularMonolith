using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModularMonolith.Modules.Conferences.Core.DTO;

namespace ModularMonolith.Modules.Conferences.Core.Services
{
    public interface IHostService
    {
        Task<HostDetailsDto> GetAsync(Guid id);
        Task<IReadOnlyList<HostDto>> BrowseAsync();
        Task AddAsync(HostDetailsDto dto);
        Task UpdateAsync(HostDetailsDto dto);
        Task DeleteAsync(Guid id);
    }
}