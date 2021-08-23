using System;
using ModularMonolith.Shared.Abstractions.Events;

namespace ModularMonolith.Modules.Tickets.Core.Events.External
{
    internal record ConferenceCreated(Guid Id, string ConferenceName, int? ParticipantsLimit) : IEvent;
}