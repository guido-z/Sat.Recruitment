using Microsoft.AspNetCore.Mvc.Testing;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Integration
{
    public class UsersControllerTests
    {
        private readonly WebApplicationFactory<Program> factory =
            new WebApplicationFactory<Program>();

        [Fact]
        public async Task CreateUserAsync_InvalidModel_ReturnsBadRequest()
        {
            var model = new UserViewModel
            {
                Name = "Mike",
                Email = "mikegmail.com", // invalid email address
                Address = "Av. Juan G",
                Phone = "+349 1122354215",
                UserType = "Normal",
                Money = 124
            };

            HttpClient client = factory.CreateClient();

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_ExistingUser_ReturnsBadRequest()
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

            HttpClient client = factory.CreateClient();

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);

            Assert.False(response.IsSuccessStatusCode);
        }

        [Fact]
        public async Task CreateUserAsync_NewUser_ReturnsOk()
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

            string filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files/Users.txt");

            string text = await File.ReadAllTextAsync(filePath);

            HttpClient client = factory.CreateClient();

            var content = new StringContent(
                JsonSerializer.Serialize(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);

            // restore file to its previous state.
            await File.WriteAllTextAsync(filePath, text);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}
