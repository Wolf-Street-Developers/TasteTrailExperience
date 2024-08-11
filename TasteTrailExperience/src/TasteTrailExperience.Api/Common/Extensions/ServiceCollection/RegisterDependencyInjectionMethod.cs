using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Core.Menus.Services;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Core.Venues.Services;
using TasteTrailExperience.Infrastructure.Menus.Repositories;
using TasteTrailExperience.Infrastructure.Menus.Services;
using TasteTrailExperience.Infrastructure.Venues.Repositories;
using TasteTrailExperience.Infrastructure.Venues.Services;

namespace TasteTrailExperience.Api.Common.Extensions.ServiceCollection;

public static class RegisterDependencyInjectionMethod
{
    public static void RegisterDpInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IVenueRepository, VenueEfCoreRepository>();
        serviceCollection.AddTransient<IMenuRepository, MenuEfCoreRepository>();

        serviceCollection.AddTransient<IVenueService, VenueService>();
        serviceCollection.AddTransient<IMenuService, MenuService>();
    } 
}
