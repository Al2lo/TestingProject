using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TestingProject.BLL.DTOs;
using TestingProject.BLL.Services;
using TestingProject.BLL.Services.Interfaces;
using TestingProject.BLL.Validators;

namespace TestingProject.BLL.Configuration
{
    public static class DependencyInjections
    {
        public static IServiceCollection AddBLL(this IServiceCollection services)
        {
            services.AddScoped<IReceiverSpeedDataService, ReceiverSpeedDataService>();
            services.AddScoped<ISenderSpeedDataService, SenderSpeedDataService>();
            services.AddTransient<IValidator<AddSpeedDataDTO>, SpeedDataValidator>();

            return services;
        }
    }
}
