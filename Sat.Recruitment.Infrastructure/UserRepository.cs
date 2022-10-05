using Microsoft.Extensions.Configuration;
using Sat.Recruitment.Core;
using Sat.Recruitment.Domain;
using Sat.Recruitment.Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Sat.Recruitment.Infrastructure
{
    public sealed class UserRepository : IUserRepository, IDisposable
    {
        private readonly SemaphoreSlim semaphore = new SemaphoreSlim(1);
        private readonly ICsvSerializer<User> serializer = new UserSerializer();
        private readonly string filePath;

        public UserRepository(IConfiguration config)
        {
            filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                config["RelativeFilePath"]);
        }

        public async IAsyncEnumerable<User> GetUsersAsync(CancellationToken cancellationToken = default)
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
                cancellationToken.ThrowIfCancellationRequested();
                yield return serializer.Deserialize(line);
            }
        }

        public async Task CreateUserAsync(User user)
        {
            string line = serializer.Serialize(user);

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
