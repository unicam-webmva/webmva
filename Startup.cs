﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using webmva.Data;


namespace webmva
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
            if (Globals.TIPODB.Equals("sqlite"))
                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlite(Globals.CONNECTIONSTRING));
            else if (Globals.TIPODB.Equals("sqlserver"))
                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlServer(Globals.CONNECTIONSTRING));
            else if (Globals.TIPODB.Equals("inmemory"))
                services.AddDbContext<MyDbContext>(options =>
                    options.UseInMemoryDatabase(Globals.CONNECTIONSTRING));
            else
                services.AddDbContext<MyDbContext>(options =>
                    options.UseSqlite("Data Source=webmva.db"));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
        }
    }
}
