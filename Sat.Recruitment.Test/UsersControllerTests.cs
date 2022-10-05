using Microsoft.AspNetCore.Mvc;

using Sat.Recruitment.Api.Controllers;
using Sat.Recruitment.Api.Models;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test
{
    [CollectionDefinition("Tests", DisableParallelization = true)]
    public class UsersControllerTests
    {
        [Fact]
        public async Task CreateUser_UserDoesNotExist_ReturnsOk()
        {
            var userController = new UsersController();

            var model = new UserViewModel
            {
                Name = "Mike",
                Email = "mike@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            IActionResult result = await userController.CreateUser(model);

            Assert.IsAssignableFrom<OkObjectResult>(result);
        }

        [Fact]
        public async Task CreateUser_UserExists_ReturnsBadRequest()
        {
            var userController = new UsersController();

            var model = new UserViewModel
            {
                Name = "Agustina",
                Email = "Agustina@gmail.com",
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            IActionResult result = await userController.CreateUser(model);

            Assert.IsAssignableFrom<BadRequestObjectResult>(result);            
        }
    }
}
