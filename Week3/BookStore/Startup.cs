using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BookStore.DbOperations;
using Microsoft.OpenApi.Models;
using BookStore.Middlewares;
using BookStore.Services;
using BookStore.Attributes;
using static System.Net.WebRequestMethods;
using BookStore.DbOperations;

namespace BookStore
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "BookStore", Version = "v1" });
            });


            //Ramda kullanılması için örnek bir db (sanal) oluşturma
            services.AddDbContext<BookStoreDbContext>(options => options.UseInMemoryDatabase(databaseName: "BookStoreDB"));

            // FakeBookService'yi eklemek için yapılandırma
            services.AddScoped<IBookService, FakeBookService>();
        }

        //Bu metod, HTTP istemcilerinin gelen isteklere nasıl yanıt vereceğini yapılandırır.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BookStore v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            // RequestLoggingMiddleware ve GlobalExceptionMiddleware
            app.UseMiddleware<RequestLoggingMiddleware>();
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
