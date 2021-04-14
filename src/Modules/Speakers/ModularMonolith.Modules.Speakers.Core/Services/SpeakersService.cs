using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ModularMonolith.Modules.Speakers.Core.DAL.Repositories;
using ModularMonolith.Modules.Speakers.Core.DTO;
using ModularMonolith.Modules.Speakers.Core.Exceptions;
using ModularMonolith.Modules.Speakers.Core.Mappings;

namespace ModularMonolith.Modules.Speakers.Core.Services
{
    internal sealed class SpeakersService : ISpeakersService
    {
        private readonly ISpeakersRepository _repository;

        public SpeakersService(ISpeakersRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<SpeakerDto>> BrowseAsync()
        {
            var entities = await _repository.BrowseAsync();
            return entities?.Select(e => e.AsDto());
        }

        public async Task<SpeakerDto> GetAsync(Guid speakerId)
        {
            var entity = await _repository.GetAsync(speakerId);
            return entity?.AsDto();
        }

        public async Task CreateAsync(SpeakerDto speaker)
        {
            var alreadyExists = await _repository.ExistsAsync(speaker.Id);
            if (alreadyExists)
            {
                throw new SpeakerAlreadyExistsException(speaker.Id);
            }

            await _repository.AddAsync(speaker.AsEntity());
        }

        public async Task UpdateAsync(SpeakerDto speaker)
        {
            var exists = await _repository.ExistsAsync(speaker.Id);

            if (!exists)
            {
                throw new SpeakerNotFoundException(speaker.Id);
            }
            
            await _repository.UpdateAsync(speaker.AsEntity());
        }
    }
}