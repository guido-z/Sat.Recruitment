using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Domain;
using System;

namespace Sat.Recruitment.Api.Mappers
{
    static class UserMappingExtensions
    {
        public static User MapToDomainUser(this UserViewModel model)
        {
            var type = (UserType)Enum.Parse(typeof(UserType), model.UserType);
            
            User user = type switch
            {
                UserType.Normal => new NormalUser(model.Money),
                UserType.SuperUser => new SuperUser(model.Money),
                _ => new PremiumUser(model.Money)
            };

            user.Name = model.Name;
            user.Email = model.Email;
            user.Address = model.Address;
            user.Phone = model.Phone;
            return user;
        }
    }
}
