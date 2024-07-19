using Microsoft.Extensions.DependencyInjection;

namespace TestingProject.DAL.Configuration
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddDAL(this IServiceCollection services)
        {
            return services;
        }
    }
}
