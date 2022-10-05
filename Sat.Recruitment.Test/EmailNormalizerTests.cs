using Sat.Recruitment.Api.Exceptions;
using Sat.Recruitment.Api.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class EmailNormalizerTests
    {
        [Theory]
        [InlineData("mike@gmail.com")]
        [InlineData("Agustina@gmail.com")]
        public void NormalizeEmail_NormalizedEmail_ReturnsSameEmail(string email)
        {
            string result = EmailNormalizer.NormalizeEmail(email);
            Assert.Equal(email, result);
        }

        [Theory]
        [InlineData("mik.e@gmail.com", "mike@gmail.com")]
        [InlineData("mike+plus@gmail.com", "mikeplus@gmail.com")]
        [InlineData("m.i.k.e+plus@gmail.com", "mikeplus@gmail.com")]
        public void NormalizeEmail_NotNormalEmail_ReturnsNormalizedEmail(string email, string expected)
        {
            string result = EmailNormalizer.NormalizeEmail(email);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void NormalizeEmail_InvalidEmail_ThrowsException()
        {
            Assert.Throws<InvalidEmailException>(
                () => EmailNormalizer.NormalizeEmail("mikegmail.com"));
        }
    }
}
