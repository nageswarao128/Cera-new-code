using CERA.AuthenticationService;
using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.CloudService;
using CERA.Converter;
using CERA.DataOperation;
using CERA.DataOperation.Data;
using CERA.Logging;
using CERA.Platform;
using CERAAPI.Data;
using CERAAPI.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace CERASyncAPI
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
            services.AddControllers().AddNewtonsoftJson(options =>
                                        options.SerializerSettings.ReferenceLoopHandling
                                        = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddSwaggerGen();
            services.AddTransient<ICeraAzureApiService, CeraAzureApiService>();
            services.AddTransient<ICeraAwsApiService, CeraAWSApiService>();
            //services.AddDbContext<CeraDbContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera; Integrated Security= true;"));
            services.AddDbContext<CeraClientAuthenticatorContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddDbContext<CeraSpContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddDbContext<CeraDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddTransient<ICeraConverter, CeraConverter>();
            services.AddTransient<ICeraClientAuthenticator, CeraClientAuthenticator>();
            services.AddTransient<ICeraDataOperation, CERADataOperation>();
            services.AddLogging(logging => logging.AddConsole());
            services.AddSingleton<ICeraLogger, CERALogger>();

            services.AddTransient<ICeraCloudApiService, CeraCloudApiService>();
            services.AddTransient<ICeraPlatform, CeraCloudApiService>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CeraClientAuthenticatorContext>();


            //services.AddDbContext<CeraAPIUserDbContext>(x => x.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=db_Cera_User; Integrated Security= true;"));
            services.AddDbContext<CeraAPIUserDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CERA API V1");
            });

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            app.UseCookiePolicy();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
