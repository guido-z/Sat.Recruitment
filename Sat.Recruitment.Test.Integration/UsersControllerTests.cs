using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Sat.Recruitment.Api;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Results;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Sat.Recruitment.Test.Integration
{
    public class UsersControllerTests : IDisposable
    {
        private readonly WebApplicationFactory<Program> factory =
            new WebApplicationFactory<Program>();

        private readonly string filePath;
        private readonly string initialText;

        public UsersControllerTests()
        {
            filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "Files/Users.txt");

            initialText = File.ReadAllText(filePath);
        }

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
                JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseContent);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Single(result.Errors);
            Assert.Equal("Please enter a valid email address", result.Errors[0]);
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
                JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseContent);

            Assert.False(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.False(result.IsSuccess);
            Assert.Null(result.Data);
            Assert.Single(result.Errors);
            Assert.Equal("User Agustina@gmail.com alerady exists", result.Errors[0]);
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

            HttpClient client = factory.CreateClient();
                        
            var content = new StringContent(
                JsonConvert.SerializeObject(model),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            HttpResponseMessage response = await client.PostAsync("/users", content);
            var responseContent = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<Result>(responseContent);
           
            Assert.True(response.IsSuccessStatusCode);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.True(result.IsSuccess);
            Assert.NotNull(result.Data);
            Assert.Empty(result.Errors);
        }

        public void Dispose()
        {
            File.WriteAllText(filePath, initialText);
        }
    }
}
