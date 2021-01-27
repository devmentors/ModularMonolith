using System;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Modules.Conferences.Core.Exceptions
{
    internal class HostNotFoundException : CustomException
    {
        public HostNotFoundException(Guid hostId) : base($"Host with ID: '{hostId}' was not found.")
        {
        }
    }
}