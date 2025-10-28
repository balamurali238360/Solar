using SunTrack.API.Services.Leads;
namespace SunTrack.API
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<ILeadsService, LeadsService>();
            return services;
        }
            
    }
}
