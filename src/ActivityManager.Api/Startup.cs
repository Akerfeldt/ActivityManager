using System;
using ActivityManager.Api.HostedServices;
using ActivityManager.Data;
using ActivityManager.Services.Caches;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ActivityManager.Api
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
#if DEBUG
            // in production, front end will be hosted on the same domain or requests will come through a proxy
            // so CORS is only needed for running locally
            services.AddCors(options =>
                {
                    options.AddDefaultPolicy(
                        builder =>
                        {
                            builder.AllowAnyHeader();
                            builder.AllowAnyMethod();
                            builder.AllowAnyOrigin();
                        });
                });
#endif

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers()
                .AddFluentValidation(fv =>
                {
                    fv.ImplicitlyValidateChildProperties = true;
                    fv.RegisterValidatorsFromAssembly(typeof(Services.CreateActivityValidator).Assembly);
                });

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    // base-address of your identityserver
                    options.Authority = "https://localhost:44390";

                    // name of the API resource
                    options.ApiName = "amgr_api";
                    options.ApiSecret = "secret";

                    options.EnableCaching = false;
                });

            services.AddDistributedMemoryCache();
            services.AddMediatR(typeof(Services.CreateActivityHandler).Assembly);
            services.AddScoped<ILookupsCache, LookupsCache>();

            // prime the cache before WebHost starts accepting requests
            services.AddHostedService<CachePrimerService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseCors();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
