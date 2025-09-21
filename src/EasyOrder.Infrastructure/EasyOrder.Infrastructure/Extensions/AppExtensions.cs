using EasyOrder.Application.Consumers;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;

namespace EasyOrder.Infrastructure.Extensions;

public static class AppExtensions
{        
    public static void AddRabbitMQService(this IServiceCollection services)
    {
        services.AddMassTransit(busConfiguration =>
        {            
            busConfiguration.AddConsumer<OrderCreatedEventConsumer>();
            
            busConfiguration.UsingRabbitMq((ctx, cfg) =>
            {
                cfg.Host(new Uri("amqp://localhost:5672"), host =>
                {
                    host.Username("guest");
                    host.Password("guest");
                });

                cfg.ConfigureEndpoints(ctx);
            });
        });
    }
}