using System;
using Xunit;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Test
{
    public abstract class BaseTest
    {
        public BaseTest()
        {

        }
    }

    public class DbTest : IDisposable
    {
        private string dataBaseName = $"dbApiTest_{Guid.NewGuid().ToString().Replace("-", string.Empty)}";
        public ServiceProvider ServiceProvider { get; private set; }

        public DbTest()
        {
            var stringConnection = $"Persist Security Info=True;Server=localhost;Port=3306;Database=DATABASE_TEST_NAME;Uid=root;Pwd=86801qaz";
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddDbContext<MyContext>(o => o.UseMySql(stringConnection.Replace("DATABASE_TEST_NAME", dataBaseName), new MySqlServerVersion(new Version(8, 0, 21))), ServiceLifetime.Transient);
            ServiceProvider = serviceCollection.BuildServiceProvider();
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureCreated();
            }
        }
        public void Dispose()
        {
            using (var context = ServiceProvider.GetService<MyContext>())
            {
                context.Database.EnsureDeleted();
            }
        }
    }
}
