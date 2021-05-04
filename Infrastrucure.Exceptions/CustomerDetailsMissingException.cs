using System;

namespace Infrastrucure.Exceptions
{
    public class CustomerDetailsMissingException : Exception
    {
        public CustomerDetailsMissingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public CustomerDetailsMissingException(string message) : base(message)
        {
        }
    }
}
