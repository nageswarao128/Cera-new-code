using CERA.AuthenticationService;
using CERA.AWS.CloudService;
using CERA.Azure.CloudService;
using CERA.CloudService;
using CERA.Converter;
using CERA.DataOperation;
using CERA.DataOperation.Data;
using CERA.Logging;
using CERA.Platform;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERADataAPI
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
            services.AddSwaggerGen();
            services.AddTransient<ICeraAzureApiService, CeraAzureApiService>();
            services.AddTransient<ICeraAwsApiService, CeraAWSApiService>();
            services.AddDbContext<CeraDbContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddDbContext<CeraSpContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddDbContext<CeraClientAuthenticatorContext>(x => x.UseSqlServer(Configuration.GetConnectionString("DbCS")));
            services.AddTransient<ICeraClientAuthenticator, CeraClientAuthenticator>();

            services.AddTransient<ICeraConverter, CeraConverter>();
            services.AddTransient<ICeraDataOperation, CERADataOperation>();
            services.AddLogging(logging => logging.AddConsole());
            services.AddSingleton<ICeraLogger, CERALogger>();

            services.AddTransient<ICeraCloudApiService, CeraCloudApiService>();
            services.AddTransient<ICeraPlatform, CeraCloudApiService>();
            
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<CeraClientAuthenticatorContext>();

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:key"])),
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   RequireExpirationTime = true
               };
           });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "CERA Get Call API V1");
            });

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
