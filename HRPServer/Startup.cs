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
                opt.UseInMemoryDatabase("hrp");
            });
            services.AddMvc().AddNewtonsoftJson(opt =>
            {
                opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
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

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapODataRoute("odata", null, this.BuildEdm(app));
            });
            app.UseODataBatching();
            
        }

        private IEdmModel BuildEdm(IApplicationBuilder app)
        {
            var optionsbuilder = new DbContextOptionsBuilder<HRPModel>();
            optionsbuilder.UseInMemoryDatabase("hrp");
            var dataAdapter = new OeEfCoreDataAdapter<HRPModel>(optionsbuilder.Options);
            return dataAdapter.BuildEdmModel();
            
        }
    }
}
