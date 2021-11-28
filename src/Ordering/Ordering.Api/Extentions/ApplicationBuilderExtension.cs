using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Api.RabitMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.Api.Extentions
{
    static public class ApplicationBuilderExtension
    {
        public static EventBusRabbitMQConsumer Listener { get; set; }
        public static IApplicationBuilder UseRabbitListner(this IApplicationBuilder app)
        {
            Listener = app.ApplicationServices.GetService<EventBusRabbitMQConsumer>();
            var life = app.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarting);
            life.ApplicationStopping.Register(OnStopping);


            return app;
        }

        public static void OnStarting()
        {
            Listener.Consume();
        }
        public static void OnStopping()
        {
            Listener.Disconnect();
        }
    }
}
