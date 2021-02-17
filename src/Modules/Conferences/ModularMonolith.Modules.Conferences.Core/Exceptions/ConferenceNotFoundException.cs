using System;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Modules.Conferences.Core.Exceptions
{
    internal class ConferenceNotFoundException : CustomException
    {
        public Guid ConferenceId { get; }

        public ConferenceNotFoundException(Guid conferenceId) : base($"Conference with ID: '{conferenceId}' was not found.")
        {
            ConferenceId = conferenceId;
        }
    }
}