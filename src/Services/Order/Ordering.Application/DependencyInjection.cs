﻿using BuildingBlocks.Behaviour;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register application services here
            // Example: services.AddScoped<IOrderService, OrderService>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
                config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            }
            );

            return services;
        }
    }
}
