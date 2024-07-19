using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestingProject.BLL.Services;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.BLL.Validators;
using TestingProject.DAL.Entities;

namespace TestingProject.BLL.Configuration
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IReceiverSpeedDataService, ReceiverSpeedDataService>();
            services.AddScoped<ISenderSpeedDataService, SenderSpeedDataService>();
            services.AddTransient<IValidator<SpeedData>, SpeedDataValidator>();

            return services;
        }
    }
}
