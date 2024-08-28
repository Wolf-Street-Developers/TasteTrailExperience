using TasteTrailData.Core.MenuItems.Models;
using TasteTrailData.Infrastructure.Blob.Managers;
using TasteTrailExperience.Core.Feedbacks.Repositories;
using TasteTrailExperience.Core.Feedbacks.Services;
using TasteTrailExperience.Core.MenuItems.Repositories;
using TasteTrailExperience.Core.MenuItems.Services;
using TasteTrailExperience.Core.Menus.Repositories;
using TasteTrailExperience.Core.Menus.Services;
using TasteTrailExperience.Core.Venues.Repositories;
using TasteTrailExperience.Core.Venues.Services;
using TasteTrailExperience.Infrastructure.Feedbacks.Repositories;
using TasteTrailExperience.Infrastructure.Feedbacks.Services;
using TasteTrailExperience.Infrastructure.MenuItems.Managers;
using TasteTrailExperience.Infrastructure.MenuItems.Repositories;
using TasteTrailExperience.Infrastructure.MenuItems.Services;
using TasteTrailExperience.Infrastructure.Menus.Repositories;
using TasteTrailExperience.Infrastructure.Menus.Services;
using TasteTrailExperience.Infrastructure.Venues.Managers;
using TasteTrailExperience.Infrastructure.Venues.Repositories;
using TasteTrailExperience.Infrastructure.Venues.Services;

namespace TasteTrailExperience.Api.Common.Extensions.ServiceCollection;

public static class RegisterDependencyInjectionMethod
{
    public static void RegisterDependencyInjection(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<IVenueRepository, VenueEfCoreRepository>();
        serviceCollection.AddTransient<IMenuRepository, MenuEfCoreRepository>();
        serviceCollection.AddTransient<IMenuItemRepository, MenuItemEfCoreRepository>();
        serviceCollection.AddTransient<IFeedbackRepository, FeedbackEfCoreRepository>();

        serviceCollection.AddTransient<IVenueService, VenueService>();
        serviceCollection.AddTransient<IMenuService, MenuService>();
        serviceCollection.AddTransient<IMenuItemService, MenuItemService>();
        serviceCollection.AddTransient<IFeedbackService, FeedbackService>();

        serviceCollection.AddTransient<VenueImageManager>();
        serviceCollection.AddTransient<MenuItemImageManager>();
        serviceCollection.AddTransient<MenuImageManager>();
    } 
}
