using System;

namespace Sat.Recruitment.Api.Exceptions
{
    public sealed class InvalidEmailException : Exception
    {
        public InvalidEmailException()
        {
        }

        public InvalidEmailException(string email)
            : base($"Email address {email} is not valid")
        {
        }
    }
}
