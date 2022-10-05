using Sat.Recruitment.Api.Exceptions;

namespace Sat.Recruitment.Api.Helpers
{
    public static class EmailNormalizer
    {
        public static string NormalizeEmail(string email)
        {
            string[] fragments = email.Split('@');

            if (fragments.Length != 2)
            {
                throw new InvalidEmailException();
            }

            var normalized = fragments[0]
                .Replace(".", string.Empty)
                .Replace("+", string.Empty);

            return string.Join('@', normalized, fragments[1]);
        }
    }
}
