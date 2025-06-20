﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Application.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure;
public static class DependencyInjection
{
     public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,IConfiguration configuration)
     {
      
         var connectionString = configuration.GetConnectionString("Database");
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();


        services.AddDbContext<ApplicationDbContext>((sp,options) =>
         {
             options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
             options.UseSqlServer(connectionString);
         }
       );


         return services;
     }
}
