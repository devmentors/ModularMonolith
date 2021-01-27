using System;

namespace ModularMonolith.Shared.Abstractions.Exceptions
{
    public class CustomException : Exception
    {
        protected CustomException(string message) : base(message)
        {
        }
    }
}