/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace RealEstatePro.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCoreServices(configuration);

            return services;
        }

        private static IServiceCollection AddCoreServices(this IServiceCollection services, IConfiguration configuration)
        {       
            return services;
        }
    }
}
