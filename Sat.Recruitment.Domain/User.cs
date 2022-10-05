namespace Sat.Recruitment.Domain
{
    public abstract class User
    {
        protected readonly decimal money;

        public User(UserType type, decimal money)
        {
            this.money = money;
            UserType = type;
        }

        public string Name { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        public UserType UserType { get; set; }

        public abstract decimal Money { get; }

        public override bool Equals(object obj)
        {
            User user = obj as User;

            if (user == null)
            {
                return false;
            }

            return user.Email == Email || user.Phone == Phone ||
                (user.Name == Name && user.Address == Address);
        }
    }
}
