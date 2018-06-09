using System;

namespace CFidelity.API.Core.CustomException
{
    public class BusinessFlowException : Exception
    {
        public string Error;

        public BusinessFlowException(string message) : base(message)
        {
            this.Error = message;
        }
    }
}