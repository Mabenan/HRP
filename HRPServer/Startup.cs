using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.OData.Edm;
using Microsoft.AspNet.OData.Builder;
using HRPData.Context;
using Microsoft.EntityFrameworkCore;
using HRPData.Entity;
using OdataToEntity.EfCore;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using OdataToEntity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using HRPServer.Controller;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.Sqlite;

namespace HRPServer
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplicationInsightsTelemetry();
            services.AddOData();
            services.AddDbContext<HRPModel>(opt =>
            {
                var connection = new SqliteConnection("Filename=HRPServer.db");

                connection.Open();
                opt.UseSqlite(connection, b =>
                {
                    b.MigrationsAssembly("HRPServer");
                });

        });

            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }) .AddRazorPagesOptions(options =>
                {
                    options.Conventions.AuthorizeAreaFolder("Identity", "/Account/Manage");
                    options.Conventions.AuthorizeAreaPage("Identity", "/Account/Logout");
                    options.Conventions.AddAreaPageRoute("Home", "/Index", "");
                });

            services.AddControllersWithViews();
            services.AddRazorPages();


            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = $"/Identity/Account/Login";
                options.LogoutPath = $"/Identity/Account/Logout";
                options.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseRouting();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
                endpoints.MapODataRoute("odata", "odata", this.BuildEdm(app));
            });
            app.UseODataBatching();
            
        }

        private IEdmModel BuildEdm(IApplicationBuilder app)
        {
            var optionsbuilder = new DbContextOptionsBuilder<HRPModel>();
            optionsbuilder.UseInMemoryDatabase("hrp");
            var dataAdapter = new OeEfCoreDataAdapter<HRPModel>(optionsbuilder.Options);
            return dataAdapter.BuildEdmModel(new[] { 
                typeof(HRPUser),
                typeof(IdentityUser<string>),
                typeof(IdentityUser),
                typeof(IdentityUserRole<string>),
                typeof(IdentityRole<string>),
                typeof(IdentityRoleClaim<string>),
                typeof(IdentityUserClaim<string>),
                typeof(IdentityUserLogin<string>),
                typeof(IdentityUserToken<string>),
                typeof(IdentityRole)
            });
            
        }
    }
}
