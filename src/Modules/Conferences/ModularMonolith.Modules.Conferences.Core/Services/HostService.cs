using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModularMonolith.Modules.Conferences.Core.DTO;
using ModularMonolith.Modules.Conferences.Core.Entities;
using ModularMonolith.Modules.Conferences.Core.Exceptions;
using ModularMonolith.Modules.Conferences.Core.Repositories;

namespace ModularMonolith.Modules.Conferences.Core.Services
{
    internal class HostService : IHostService
    {
        private readonly IHostRepository _hostRepository;

        public HostService(IHostRepository hostRepository)
        {
            _hostRepository = hostRepository;
        }

        public async Task AddAsync(HostDetailsDto dto)
        {
            dto.Id = Guid.NewGuid();
            var host = new Host
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description
            };
            await _hostRepository.AddAsync(host);
        }

        public async Task UpdateAsync(HostDetailsDto dto)
        {
            var host = await _hostRepository.GetAsync(dto.Id);
            if (host is null)
            {
                throw new HostNotFoundException(dto.Id);
            }

            host.Name = dto.Name;
            host.Description = dto.Description;
            await _hostRepository.UpdateAsync(host);
        }

        public async Task DeleteAsync(Guid id)
        {
            var host = await _hostRepository.GetAsync(id);
            if (host is null)
            {
                throw new HostNotFoundException(id);
            }

            await _hostRepository.DeleteAsync(host);
        }

        public async Task<HostDetailsDto> GetAsync(Guid id)
        {
            var host = await _hostRepository.GetAsync(id);

            return host is not null
                ? new HostDetailsDto
                {
                    Id = host.Id,
                    Name = host.Name,
                    Description = host.Description
                }
                : default;
        }

        public async Task<IReadOnlyList<HostDto>> BrowseAsync()
        {
            var hosts = await _hostRepository.BrowseAsync();

            return hosts.Select(x => new HostDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }
    }
}