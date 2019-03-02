using Auga.BL.Authorize;
using Auga.BL.ItemsService;
using Auga.BL.ProfileService;
using Auga.BL.UsersConnections;
using Auga.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Auga.Extensions
{
    public static class ConfigureContainerExtensions
    {
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        public static void AddScopedServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<ITokenService, TokenService>();
            serviceCollection.AddScoped<IProfileService, ProfileService>();
            serviceCollection.AddScoped<IItemService, ItemService>();
            serviceCollection.AddScoped<IConnectionMapping, ConnectionMapping>();
            serviceCollection.AddScoped<IMatchmakingService, MatchmakingService>();
        }
    }
}
