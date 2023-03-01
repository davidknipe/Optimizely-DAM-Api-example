using Foundation.Features.OptimizelyDAM.REST;
using Microsoft.Extensions.DependencyInjection;

namespace Foundation.Features.OptimizelyDAM.Initilization
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDamApi(this IServiceCollection services)
        {
            services.AddTransient<IDamClient, DamClient>();
        }
    }
}
