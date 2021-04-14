using System;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Modules.Speakers.Core.Exceptions
{
    public sealed class SpeakerAlreadyExistsException : CustomException
    {
        public Guid Id { get; }

        public SpeakerAlreadyExistsException(Guid id) : base($"Speaker with id: '{id}' already exists.")
            => Id = id;
    }
}