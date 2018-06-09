using System;

namespace CFidelity.API.Core.CustomException
{
    public class UnauthorizedException : Exception
    {
        public new string Message;

        public UnauthorizedException(string message) : base()
        {
            this.Message = message;
        }
    }
}