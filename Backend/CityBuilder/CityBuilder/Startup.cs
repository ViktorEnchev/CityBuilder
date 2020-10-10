using CityBuilder.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using CityBuilder.Services;
using AutoMapper;
using CityBuilder.Middleware;
using System.Text.Json;
using CityBuilder.Models.DTOs.IdentityDTOs;
using System.Text;
using CityBuilder.Extensions;

namespace CityBuilder.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<CityBuilderDbContext>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));

            var indentityConfig = Configuration.GetSection("IdentityConfig");

            services.Configure<IdentityConfig>(indentityConfig);

            services.AddJwtAuthentication(indentityConfig.Get<IdentityConfig>());

            services.AddScoped<CityService>();
            services.AddScoped<RoadService>();
            services.AddScoped<IdentityService>();

            services.AddAutoMapper(typeof(Program));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<CityBuilderDbContext>();
                context.Database.Migrate();
            }
        }
    }
}
