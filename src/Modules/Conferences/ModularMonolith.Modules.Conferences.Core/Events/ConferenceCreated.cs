using System;
using ModularMonolith.Shared.Abstractions.Events;

namespace ModularMonolith.Modules.Conferences.Core.Events
{
    internal record ConferenceCreated(Guid Id, string Name, int? ParticipantsLimit) : IEvent;
}