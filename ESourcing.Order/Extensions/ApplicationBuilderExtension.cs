using ESourcing.Orders.Consumers;
using Microsoft.Extensions.Hosting;

namespace ESourcing.Orders.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static EventBusOrderCreateConsumer Listener { get; set; }

        public static IApplicationBuilder UseEventBusListener(this IApplicationBuilder application)
        {

            Listener = application.ApplicationServices.GetService<EventBusOrderCreateConsumer>();

            var life = application.ApplicationServices.GetService<IHostApplicationLifetime>();

            life.ApplicationStarted.Register(OnStarted);
            life.ApplicationStopping.Register(OnStopping);

            return application;
        }

        private static void OnStarted()
        {
            Listener.Consume();
        }

        private static void OnStopping()
        {
            Listener.Disconnect();
        }

    }
}
