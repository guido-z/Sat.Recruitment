namespace Sat.Recruitment.Domain
{
    public class SuperUser : User
    {
        public SuperUser(string name, string email, string address, string phone, decimal money)
            : base(name, email, address, phone, UserType.SuperUser, money)
        {
        }

        public override decimal Money
        {
            get
            {
                decimal percentage = 0;

                if (money > 100)
                {
                    percentage = 0.20M;
                }

                var gif = money * percentage;
                return money + gif;
            }
        }
    }
}
