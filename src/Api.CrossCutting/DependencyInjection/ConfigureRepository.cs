using System;
using Api.Data.Context;
using Api.Data.Implementations;
using Api.Data.Repository;
using Api.Data.Utils;
using Api.Domain.Interfaces;
using Api.Domain.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(
            IServiceCollection serviceCollection
        )
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();
            serviceCollection.AddScoped<IStateRepository, StateImplementation>();
            serviceCollection.AddScoped<ICityRepository, CityImplementation>();
            serviceCollection.AddScoped<IZipCodeRepository, ZipCodeImplementation>();

            if (Constants.DataBase.ToUpper().Equals("MYSQL"))
                serviceCollection.AddDbContext<MyContext>(options => options.UseMySql(Constants.ConnectionStringMySql, new MySqlServerVersion(new Version(8, 0, 21))));//assim
            else
                serviceCollection.AddDbContext<MyContext>(options => options.UseSqlServer(Environment.GetEnvironmentVariable("DB_CONNECTION_SQLSERVER"))); //ou assim
        }
    }
}
