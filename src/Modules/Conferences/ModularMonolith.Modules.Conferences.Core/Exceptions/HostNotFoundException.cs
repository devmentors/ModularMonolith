using System;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Modules.Conferences.Core.Exceptions
{
    internal class HostNotFoundException : CustomException
    {
        public Guid HostId { get; }

        public HostNotFoundException(Guid hostId) : base($"Host with ID: '{hostId}' was not found.")
        {
            HostId = hostId;
        }
    }
}