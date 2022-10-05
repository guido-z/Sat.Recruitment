using Microsoft.AspNetCore.Mvc;
using Moq;
using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Application.Exceptions;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        private readonly Mock<IUserApplication> application = new Mock<IUserApplication>();

        [Fact]
        public async Task CreateUser_UserDoesNotExist_ReturnsOk()
        {
            var model = new UserViewModel
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            var user = new NormalUser(124)
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
            };

            application.Setup(a => a.CreateUserAsync(It.IsAny<User>()))
                .ReturnsAsync(user);

            var userController = new UsersController(application.Object);

            IActionResult result = await userController.CreateUser(model);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_UserExists_ReturnsBadRequest()
        {
            var model = new UserViewModel
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            application.Setup(a => a.CreateUserAsync(It.IsAny<User>()))
                .ThrowsAsync(new DuplicateUserException());

            var userController = new UsersController(application.Object);
            
            IActionResult result = await userController.CreateUser(model);

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);            
        }
    }
}
