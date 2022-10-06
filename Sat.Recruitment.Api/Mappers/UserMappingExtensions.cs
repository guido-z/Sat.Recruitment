using Sat.Recruitment.Api.Helpers;
using Sat.Recruitment.Api.Models;
using Sat.Recruitment.Domain;
using System;

namespace Sat.Recruitment.Api.Mappers
{
    static class UserMappingExtensions
    {
        public static User MapToDomainUser(this UserViewModel model)
        {
            var email = EmailNormalizer.NormalizeEmail(model.Email);

            return (UserType)Enum.Parse(typeof(UserType), model.UserType) switch
            {
                UserType.Normal => new NormalUser(model.Name, email, model.Address, model.Phone, model.Money),
                UserType.SuperUser => new SuperUser(model.Name, email, model.Address, model.Phone, model.Money),
                _ => new PremiumUser(model.Name, email, model.Address, model.Phone, model.Money),
            };
        }

        public static UserViewModel ToViewModel(this User user)
        {
            return new UserViewModel
            {
                Name = user.Name,
                Email = user.Email,
                Address = user.Address,
                Phone = user.Phone,
                UserType = user.UserType.ToString(),
                Money = user.Money
            };
        }
    }
}
