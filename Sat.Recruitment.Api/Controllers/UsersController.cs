using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Api.Results;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;

namespace Sat.Recruitment.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public partial class UsersController : ControllerBase
    {

        private readonly List<User> _users = new List<User>();
        public UsersController()
        {
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(UserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return ResultFactory.FromValidationErrors(ModelState);
            }

            if (model.UserType == "Normal")
            {
                if (model.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.12);
                    //If new user is normal and has more than USD100
                    var gif = model.Money * percentage;
                    model.Money += gif;
                }
                if (model.Money < 100)
                {
                    if (model.Money > 10)
                    {
                        var percentage = Convert.ToDecimal(0.8);
                        var gif = model.Money * percentage;
                        model.Money += gif;
                    }
                }
            }
            if (model.UserType == "SuperUser")
            {
                if (model.Money > 100)
                {
                    var percentage = Convert.ToDecimal(0.20);
                    var gif = model.Money * percentage;
                    model.Money += gif;
                }
            }
            if (model.UserType == "Premium")
            {
                if (model.Money > 100)
                {
                    var gif = model.Money * 2;
                    model.Money += gif;
                }
            }


            var reader = ReadUsersFromFile();

            //Normalize email
            var aux = model.Email.Split(new char[] { '@' }, StringSplitOptions.RemoveEmptyEntries);

            var atIndex = aux[0].IndexOf("+", StringComparison.Ordinal);

            aux[0] = atIndex < 0 ? aux[0].Replace(".", "") : aux[0].Replace(".", "").Remove(atIndex);

            model.Email = string.Join("@", new string[] { aux[0], aux[1] });

            while (reader.Peek() >= 0)
            {
                var line = reader.ReadLineAsync().Result;
                var user = new User
                {
                    Name = line.Split(',')[0].ToString(),
                    Email = line.Split(',')[1].ToString(),
                    Phone = line.Split(',')[2].ToString(),
                    Address = line.Split(',')[3].ToString(),
                    UserType = line.Split(',')[4].ToString(),
                    Money = decimal.Parse(line.Split(',')[5].ToString()),
                };
                _users.Add(user);
            }
            reader.Close();
            try
            {
                var isDuplicated = false;
                foreach (var user in _users)
                {
                    if (user.Email == model.Email
                        ||
                        user.Phone == model.Phone)
                    {
                        isDuplicated = true;
                    }
                    else if (user.Name == model.Name)
                    {
                        if (user.Address == model.Address)
                        {
                            isDuplicated = true;
                            throw new Exception("User is duplicated");
                        }

                    }
                }

                if (!isDuplicated)
                {
                    Debug.WriteLine("User Created");

                    return ResultFactory.FromSuccess(model);
                }
                else
                {
                    Debug.WriteLine("The user is duplicated");

                    return ResultFactory.FromErrorMessages("The user is duplicated");
                }
            }
            catch
            {
                Debug.WriteLine("The user is duplicated");
                return ResultFactory.FromErrorMessages("The user is duplicated");
            }

            return ResultFactory.FromSuccess(model);
        }
    }
    public class User
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public decimal Money { get; set; }
    }
}
