using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    public class UserRepository : IUserRepository, IDisposable
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private readonly string filePath;

        public UserRepository(IConfiguration config)
        {
            filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                config["RelativeFilePath"]);
        }

        public async IAsyncEnumerable<User> GetUsersAsync()
        {
            string[] lines;

            await semaphore.WaitAsync();

            try
            {
                lines = await File.ReadAllLinesAsync(filePath);
            }
            finally
            {
                semaphore.Release();
            }

            foreach (var line in lines)
            {
                string[] fields = line.Split(',');

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

                yield return user;
            }
        }

        public async Task CreateUserAsync(User user)
        {
            string[] fields =
            {
                user.Name,
                user.Email,
                user.Address,
                user.Phone,
                user.UserType.ToString(),
                user.Money.ToString()
            };

            var line = string.Join(',', fields);

            await semaphore.WaitAsync();

            try
            {
                await File.AppendAllTextAsync(filePath, Environment.NewLine + line);
            }
            finally
            {
                semaphore.Release();
            }
        }

        public void Dispose()
        {
            semaphore.Dispose();
        }
    }
}
