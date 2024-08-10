using System.Reflection;
using FluentValidation;

namespace TasteTrailExperience.Api.Common.Extensions.ServiceCollection;

public static class InitValidatorsMethod
{
    public static void AddValidators(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
