
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

            var name = fields[0];
            var email = fields[1];
            var address = fields[2];
            var phone = fields[3];
            var type = fields[4];
            var money = decimal.Parse(fields[5]);

            return (UserType)Enum.Parse(typeof(UserType), type) switch
            {
                UserType.Normal => new NormalUser(name, email, address, phone, money),
                UserType.SuperUser => new SuperUser(name, email, address, phone, money),
                _ => new PremiumUser(name, email, address, phone, money)
            };
        }        
    }
}
