using Sat.Recruitment.Domain;
using System;

namespace Sat.Recruitment.Application.Exceptions
{
    public class DuplicateUserException : Exception
    {
        public DuplicateUserException()
        {
        }

        public DuplicateUserException(User user)
            : base($"User {user.Email} alerady exists")
        {
        }
    }
}
