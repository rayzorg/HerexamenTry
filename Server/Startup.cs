using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using HerexamenTry.Server.Data;
using Microsoft.EntityFrameworkCore;
using System;
using HerexamenTry.Shared.Services;
using HerexamenTry.Server.Services;

namespace HerexamenTry.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddControllersWithViews()
    .AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);

            services.AddHttpContextAccessor();
            services.AddScoped<IBegeleiderService, BegeleiderService>();
            services.AddScoped<IJongereService, JongereService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IReactieService, ReactieService>();
            services.AddDbContext<HerexamenContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
                    sqlServerOptionsAction: sqloptions => {
                        sqloptions.EnableRetryOnFailure();

                    });
               }
               
                );
            
               
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = "https://dev-6anxoci7.us.auth0.com/";
                options.Audience = "https://dev-6anxoci7.us.auth0.com/api/v2/";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            services.AddAuth0AuthenticationClient(config =>
            {
                config.Domain = "https://dev-6anxoci7.us.auth0.com/";
                config.ClientId = "nWFUXFYI8C8N2eQz3mQv7gkherBAIb2t";
                config.ClientSecret = "1116WO3FMCS4cQYZCNz1lsTggyiwYaoNtgpPIi_gQRdqPMZ-OLHwQDyrDCyb64ep";
            });

            services.AddAuth0ManagementClient().AddManagementAccessToken();
            services.AddDatabaseDeveloperPageExceptionFilter();
          //  services.AddControllersWithViews();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseBlazorFrameworkFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
