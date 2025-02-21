﻿using Microsoft.Extensions.Logging;
using Moq;
using Sat.Recruitment.Application;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    public class UserApplicationTests
    {
        private readonly Mock<IUserRepository> repository = new Mock<IUserRepository>();
        private readonly ILogger<UserApplication> logger = Mock.Of<ILogger<UserApplication>>();

        [Fact]
        public async Task CreateUserAsync_ExistingUser_ThrowsException()
        {
            User user = new NormalUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", 124);

            var users = new List<User> { user };

            repository.Setup(r => r.GetUsersAsync(CancellationToken.None))
                .Returns(users.ToAsyncEnumerable());

            var application = new UserApplication(repository.Object, logger);

            await Assert.ThrowsAsync<DuplicateUserException>(
                () => application.CreateUserAsync(user, CancellationToken.None));
        }

        [Fact]
        public async Task CreateUserAsync_NewUser_ReturnsCreatedUser()
        {
            repository.Setup(r => r.GetUsersAsync(CancellationToken.None))
                .Returns(Enumerable.Empty<User>().ToAsyncEnumerable());

            repository.Setup(r => r.CreateUserAsync(It.IsAny<User>()))
                .Returns(Task.CompletedTask);

            var application = new UserApplication(repository.Object, logger);

            User user = new NormalUser("Agustina", "Agustina@gmail.com", "Av. Juan G", "+349 1122354215", 124);

            User result = await application.CreateUserAsync(user, CancellationToken.None);

            Assert.Equal(result.Name, user.Name);
            Assert.Equal(result.Email, user.Email);
            Assert.Equal(result.Address, user.Address);
            Assert.Equal(result.Phone, user.Phone);
            Assert.Equal(result.UserType, user.UserType);
            Assert.Equal(result.Money, user.Money);
        }
    }
}
