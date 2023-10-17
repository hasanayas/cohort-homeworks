using BookStore.DbOperations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BookStore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                //Data Generator sınıfını kullanır.
                DataGenerator.Initialize(services);
            }

            // Uygulamayı çalıştırır
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
