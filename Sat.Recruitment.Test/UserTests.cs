using Sat.Recruitment.Domain;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserTests
    {
        [Theory]
        [InlineData(1, 1)]
        [InlineData(50, 90)]
        [InlineData(200, 224)]
        public void NormalUser_GetMoney_ReturnsCorrectAmount(decimal money, decimal expected)
        {
            User user = new NormalUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", money);
            Assert.Equal(expected, user.Money);
        }

        [Theory]
        [InlineData(100, 100)]
        [InlineData(200, 240)]
        public void SuperUser_GetMoney_ReturnsCorrectAmount(decimal money, decimal expected)
        {
            User user = new SuperUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", money);
            Assert.Equal(expected, user.Money);
        }

        [Theory]
        [InlineData(100, 100)]
        [InlineData(200, 600)]
        public void PremiumUser_GetMoney_ReturnsCorrectAmount(decimal money, decimal expected)
        {
            User user = new PremiumUser("Mike", "mike@gmail.com", "Av. Juan G", "+349 1122354215", money);
            Assert.Equal(expected, user.Money);
        }
    }
}
