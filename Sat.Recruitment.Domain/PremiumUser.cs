namespace Sat.Recruitment.Domain
{
    public class PremiumUser : User
    {
        public PremiumUser(decimal money)
            : base(UserType.Premium, money)
        {
        }

        public override decimal Money => money > 100 ? money * 3 : money;
    }
}
