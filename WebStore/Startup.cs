using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebStore.Infrastructure;
using WebStore.Infrastructure.Intefaces;
using WebStore.Infrastructure.Services;

namespace WebStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
            {
                options.Filters.Add(new SimpleActionFilterAttribute());
            });

            services.AddSingleton<IProductService, InMemoryProductService>();
            //services.AddTransient<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseWelcomePage("/welcome");

            //app.Map("/index", CustomIndexHandler);

            //app.UseMiddleware<TokenMiddleware>();

            //app.UseMvcWithDefaultRoute();
            // Производим конфигурацию инфраструктуры MVC
            app.UseMvc(routes =>
            {
                // Добавляем обработчик маршрута по умолчанию
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
                // Маршрут по умолчанию состоит из трёх частей разделённых “/”
                // Первой частью указывается имя контроллера,
                // второй - имя действия (метода) в контроллере,
                // третей - опциональный параметр с именем “id”
                // Если чать не указана - используются значения по умолчанию:
                // для контроллера имя “Home”,
                // для действия - “Index”
            });

            //app.Use(async (context, next) =>
            //    {
            //        await context.Response.WriteAsync("Hello from Use middleware");
            //        //await next.Invoke();
            //    });

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello from Run middleware");
            //});

            var helloMessage = Configuration["CustomHelloWorld"];

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync(helloMessage);
            });
        }

        private void CustomIndexHandler(IApplicationBuilder app)
        {
            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("custom index handler");
            });
        }
    }
}
