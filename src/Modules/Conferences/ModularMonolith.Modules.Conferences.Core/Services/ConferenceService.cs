using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using ModularMonolith.Modules.Conferences.Core.DTO;
using ModularMonolith.Modules.Conferences.Core.Entities;
using ModularMonolith.Modules.Conferences.Core.Events;
using ModularMonolith.Modules.Conferences.Core.Exceptions;
using ModularMonolith.Modules.Conferences.Core.Repositories;
using ModularMonolith.Shared.Abstractions.Events;
using ModularMonolith.Shared.Abstractions.Messaging;

namespace ModularMonolith.Modules.Conferences.Core.Services
{
    internal class ConferenceService : IConferenceService
    {
        private readonly IConferenceRepository _conferenceRepository;
        private readonly IHostRepository _hostRepository;
        private readonly ILogger<ConferenceService> _logger;
        private readonly IMessageBroker _messageBroker;

        public ConferenceService(IConferenceRepository conferenceRepository,
            IHostRepository hostRepository, ILogger<ConferenceService> logger, IMessageBroker messageBroker)
        {
            _conferenceRepository = conferenceRepository;
            _hostRepository = hostRepository;
            _logger = logger;
            _messageBroker = messageBroker;
        }

        public async Task AddAsync(ConferenceDetailsDto dto)
        {
            await ValidateHostExistsAsync(dto.HostId);
            dto.Id = Guid.NewGuid();
            var conference = new Conference();
            Map(conference, dto);
            await _conferenceRepository.AddAsync(conference);
            await _messageBroker.PublishAsync(new ConferenceCreated(conference.Id, conference.Name,
                conference.ParticipantsLimit));
            _logger.LogInformation("Created a conference: '{Name}' with ID: '{Id}'", dto.Name, dto.Id);
        }

        public async Task<ConferenceDetailsDto> GetAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);

            return conference is not null ? MapDetails(conference) : null;
        }

        public async Task<IReadOnlyList<ConferenceDto>> BrowseAsync()
        {
            var conferences = await _conferenceRepository.BrowseAsync();

            return conferences.Select(Map<ConferenceDto>).ToList();
        }

        public async Task UpdateAsync(ConferenceDetailsDto dto)
        {
            var conference = await GetConferenceAsync(dto.Id);
            var hostId = conference.HostId;
            if (conference is null)
            {
                throw new ConferenceNotFoundException(dto.Id);
            }
            
            Map(conference, dto);
            conference.HostId = hostId; // Host cannot be updated
            await _conferenceRepository.UpdateAsync(conference);
            _logger.LogInformation("Updated a conference: '{Name}' with ID: '{Id}'", dto.Name, dto.Id);
        }

        public async Task DeleteAsync(Guid id)
        {
            // Can we delete a conference with tickets being already sold?
            var conference = await GetConferenceAsync(id);
            await _conferenceRepository.DeleteAsync(conference);
            _logger.LogInformation("Deleted a conference: '{Name}' with ID: '{Id}'", conference.Name, conference.Id);
        }

        private async Task ValidateHostExistsAsync(Guid hostId)
        {
            var host = await _hostRepository.GetAsync(hostId);
            if (host is null)
            {
                throw new HostNotFoundException(hostId);
            }
        }
        
        private async Task<Conference> GetConferenceAsync(Guid id)
        {
            var conference = await _conferenceRepository.GetAsync(id);
            if (conference is null)
            {
                throw new ConferenceNotFoundException(id);
            }

            return conference;
        }

        private static void Map(Conference conference, ConferenceDetailsDto dto)
        {
            conference.Id = dto.Id;
            conference.HostId = dto.HostId;
            conference.Name = dto.Name;
            conference.Description = dto.Description;
            conference.Location = dto.Location;
            conference.From = dto.From;
            conference.To = dto.To;
            conference.ParticipantsLimit = dto.ParticipantsLimit;
        }

        private static T Map<T>(Conference conference) where T : ConferenceDto, new()
            => new()
            {
                Id = conference.Id,
                HostId = conference.HostId,
                Name = conference.Name,
                Location = conference.Location,
                From = conference.From,
                To = conference.To,
                ParticipantsLimit = conference.ParticipantsLimit
            };

        private static ConferenceDetailsDto MapDetails(Conference conference)
        {
            var dto = Map<ConferenceDetailsDto>(conference);
            dto.Description = conference.Description;

            return dto;
        }
    }
}