using System;

namespace InternetCoast.Infrastructure.Diagnostics
{
    public class CustomEmailException : Exception
    {
        public CustomEmailException()
        {
        }

        public CustomEmailException(string message)
            : base(message)
        {
        }

        public CustomEmailException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
