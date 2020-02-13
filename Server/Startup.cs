using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Server.Hubs;
using Server.Managers;
using Server.Services;
using Server.Services.FactoryServices;

namespace Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => options.AddPolicy("CorsPolicy", 
                builder => 
                {
                    builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .WithOrigins("http://localhost:8080")
                        .AllowCredentials();
                    
                }));
            services.AddControllers();
            services.AddSignalR();

            services.AddTransient<IFactoryCreateService, FactoryCreateService>();
            services.AddSingleton<IFactoryManager, FactoryManager>();
            services.AddTransient<ICraftingService, CraftingService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors("CorsPolicy");
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute("default", 
                    "{controller}/{action=Index}/{id?}");
                endpoints.MapHub<FactoryHub>("/factory-hub");
            });
            
            app.UseHttpsRedirection();
        }
    }
}
