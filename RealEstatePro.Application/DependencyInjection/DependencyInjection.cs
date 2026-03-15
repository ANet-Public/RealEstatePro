/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using Microsoft.Extensions.DependencyInjection;
using RealEstatePro.Application.Abstractions;
using RealEstatePro.Application.Services;

namespace RealEstatePro.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddCoreServices();

            return services;
        }

        private static IServiceCollection AddCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

            return services;
        }
    }
}
