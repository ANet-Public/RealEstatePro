/*
    Copyright (c) 2026 ANet-Public
    All rights reserved.
 */

using RealEstatePro.Api.Extensions;
using RealEstatePro.Application.DependencyInjection;
using RealEstatePro.Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApi()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseApiPipeline();

app.Run();

public partial class Program;
