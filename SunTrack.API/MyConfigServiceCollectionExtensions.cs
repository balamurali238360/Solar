using SunTrack.API.Services.Customers;
using SunTrack.API.Services.Leads;
using SunTrack.API.Services.Users;
namespace SunTrack.API
{
    public static class MyConfigServiceCollectionExtensions
    {
        public static IServiceCollection AddMyDependencyGroup(this IServiceCollection services)
        {
            services.AddScoped<ILeadsService, LeadsService>();
            services.AddScoped<ICustomerService, CustomerService>();
            services.AddScoped<IUserService, UserService>();

            return services;
        }


            
    }
}
