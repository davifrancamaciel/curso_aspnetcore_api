using Api.Domain.Interfaces.Services.City;
using Api.Domain.Interfaces.Services.State;
using Api.Domain.Interfaces.Services.User;
using Api.Domain.Interfaces.Services.ZipCode;
using Api.Service.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<ILoginService, LoginService>();
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IStateService, StateService>();
            serviceCollection.AddTransient<ICityService, CityService>();
            serviceCollection.AddTransient<IZipCodeService, ZipCodeService>();
        }
    }
}
