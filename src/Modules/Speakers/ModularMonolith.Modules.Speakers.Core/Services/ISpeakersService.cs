using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ModularMonolith.Modules.Speakers.Core.DTO;

namespace ModularMonolith.Modules.Speakers.Core.Services
{
    public interface ISpeakersService
    {
        Task<IEnumerable<SpeakerDto>> BrowseAsync();
        Task<SpeakerDto> GetAsync(Guid speakerId);
        Task CreateAsync(SpeakerDto speaker);
        Task UpdateAsync(SpeakerDto speaker);
    }
}