using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TasteTrailData.Core.Roles.Models;
using TasteTrailData.Core.Users.Models;
using TasteTrailData.Infrastructure.Common.Data;

namespace TasteTrailExperience.Api.Common.Extensions.ServiceCollection;

public static class InitAspNetIdentityMethod
{
    public static void InitAspnetIdentity(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddDbContext<TasteTrailDbContext>(options =>
        {
            var connectionString = configuration.GetConnectionString("SqlConnection");
            options.UseNpgsql(connectionString);
        });

        serviceCollection.AddIdentity<User, Role>( (options) => {
            options.User.RequireUniqueEmail = true;
        })
            .AddEntityFrameworkStores<TasteTrailDbContext>()
            .AddDefaultTokenProviders()
            .AddSignInManager();
    }
}
