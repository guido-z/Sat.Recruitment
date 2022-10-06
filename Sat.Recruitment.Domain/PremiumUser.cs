namespace Sat.Recruitment.Domain
{
    public class PremiumUser : User
    {
        public PremiumUser(string name, string email, string address, string phone, decimal money)
            : base(name, email, address, phone, UserType.Premium, money)
        {
        }

        public override decimal Money => money > 100 ? money * 3 : money;
    }
}
