using System;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Modules.Speakers.Core.Exceptions
{
    public class SpeakerNotFoundException : CustomException
    {
        public Guid Id { get; }

        public SpeakerNotFoundException(Guid id) : base($"Speaker with id '{id} was not found.'")
            => Id = id;
    }
}