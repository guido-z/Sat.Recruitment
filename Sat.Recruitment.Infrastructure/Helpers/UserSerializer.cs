
using Sat.Recruitment.Domain;
using System;

namespace Sat.Recruitment.Infrastructure.Helpers
{
    class UserSerializer : ICsvSerializer<User>
    {
        public string Serialize(User obj)
        {
            string[] fields =
            {
                obj.Name,
                obj.Email,
                obj.Address,
                obj.Phone,
                obj.UserType.ToString(),
                obj.Money.ToString()
            };

            return string.Join(',', fields);
        }

        public User Deserialize(string str)
        {
            string[] fields = str.Split(',');

            var type = fields[4];
            var money = decimal.Parse(fields[5]);

            User user = (UserType)Enum.Parse(typeof(UserType), type) switch
            {
                UserType.Normal => new NormalUser(money),
                UserType.SuperUser => new SuperUser(money),
                _ => new PremiumUser(money)
            };

            user.Name = fields[0];
            user.Email = fields[1];
            user.Address = fields[2];
            user.Phone = fields[3];

            return user;
        }        
    }
}
