namespace Sat.Recruitment.Domain
{
    public class NormalUser : User
    {
        public NormalUser(decimal money)
            : base(UserType.Normal, money)
        {
        }

        public override decimal Money
        {
            get
            {
                decimal percentage = 0;

                if (money > 100)
                {
                    percentage = 0.12M;
                }
                else if (money > 10 && money < 100)
                {
                    percentage = 0.8M;
                }

                var gif = money * percentage;
                return money + gif;
            }
        }
    }
}
