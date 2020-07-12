using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InventoryAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace InventoryAPI
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
            // Install-Package Microsoft.EntityFrameworkCore.SqlServer
            // Servicio para llamada a la base de datos con DBContext
            services.AddDbContext<InventoryDB>(conection => conection.UseSqlServer(Configuration.GetConnectionString("connectionInventory")));
            services.AddTransient<InventoryService, InventoryService>();
            services.AddTransient<EncodeSecurityService, EncodeSecurityService>();
            services.AddTransient<SecurityService, SecurityService>();
            services.AddTransient<ValidationsUserService, ValidationsUserService>();
            services.AddTransient<EncodeSecurityService, EncodeSecurityService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
