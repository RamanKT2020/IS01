using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IS01.MVC
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
            services.AddControllersWithViews();

            services.AddAuthentication(o => {
                o.DefaultScheme = "Cookies";
                o.DefaultChallengeScheme = "oidc";
                })

                .AddCookie("Cookies", o =>
                {
                    o.AccessDeniedPath = "/Account/AccessDenied";
                })

                .AddOpenIdConnect("oidc", o =>
                {
                    o.ClientId = "client2";
                    o.ClientSecret = "client2_secret_code";
                    o.SignInScheme = "Cookies";
                    o.Authority = "http://localhost:5000";
                    o.RequireHttpsMetadata = false;
                    o.ResponseType = "code id_token";
                    o.SaveTokens = true;
                    o.GetClaimsFromUserInfoEndpoint = true;
                    o.Scope.Add("employeesWebApi");
                    o.Scope.Add("roles");
                    o.ClaimActions.MapUniqueJsonKey("role", "role");
                    o.TokenValidationParameters = new TokenValidationParameters
                    {
                        RoleClaimType = "role"
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
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
