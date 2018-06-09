using System;

namespace CFidelity.API.Core.CustomException
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string message = "") : base(message)
        {
        }
    }
}