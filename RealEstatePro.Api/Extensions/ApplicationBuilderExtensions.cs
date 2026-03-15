/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

namespace RealEstatePro.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static WebApplication UseApiPipeline(this WebApplication application)
        {
            if (application.Environment.IsDevelopment())
            {
                application.UseSwagger();
                application.UseSwaggerUI();
            }

            application.UseHttpsRedirection();

            application.UseAuthentication();

            application.MapControllers();

            return application;
        }
    }
}
