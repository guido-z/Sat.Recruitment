namespace Sat.Recruitment.Infrastructure.Helpers
{
    internal interface ICsvSerializer<T> where T : class
    {
        string Serialize(T obj);

        T Deserialize(string str);
    }
}
