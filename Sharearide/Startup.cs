using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sharearide.Data;
using Sharearide.Data.Repositories;
using Sharearide.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Sharearide
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<sharearideContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("sharearideContext")));
            services.AddOpenApiDocument(c => {
                c.DocumentName = "apidocs";
                c.Title = "sharearideAPI";
                c.Version = "v1";
                c.Description = "documentation for sharearideAPI";
            });

            services.AddScoped<DataInitializer>();
            services.AddScoped<IUserRepository, UserRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,DataInitializer sharearideDataInitializer)
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


            app.UseHttpsRedirection();
            app.UseSwaggerUi3();
            app.UseSwagger();
            app.UseMvc();
            sharearideDataInitializer.InitializeData().Wait();
        }
    }
}
