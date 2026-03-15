/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using Microsoft.OpenApi;

namespace RealEstatePro.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "RealEstatePro API",
                    Version = "v1",
                    Description = "API for RealEstatePro application"
                });
            });

            return services;
        }
    }
}
