using System;

namespace Sat.Recruitment.Domain
{
    public abstract class User
    {
        protected readonly decimal money;

        public User(string name, string email, string address, string phone, UserType type, decimal money)
        {
            Name = name;
            Email = email;
            Address = address;
            Phone = phone;
            UserType = type;
            this.money = money;
        }

        public string Name { get; private set; }

        public string Email { get; private set; }

        public string Address { get; private set; }

        public string Phone { get; private set; }

        public UserType UserType { get; private set; }

        public abstract decimal Money { get; }

        public override bool Equals(object obj)
        {
            var user = obj as User;

            if (user == null)
            {
                return false;
            }

            return user.Email == Email || user.Phone == Phone ||
                (user.Name == Name && user.Address == Address);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(money, Name, Email, Address, Phone, UserType, Money);
        }
    }
}
