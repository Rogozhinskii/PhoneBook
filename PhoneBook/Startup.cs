using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.DAL.Repository;
using PhoneBook.Data;
using PhoneBook.Interfaces;
using System;

namespace PhoneBook
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)=>
            services.AddDatabase(Configuration.GetSection("Database"))
                    .AddScoped(typeof(IRepository<>),typeof(DbRepository<>))
                    //.AddTransient<DbInitializer>()
                    .AddControllersWithViews()
            ;
        

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,IServiceProvider serviceProvider)
        {
            //Task.Run(async () =>
            //{
            //    using (var scope = serviceProvider.CreateScope())
            //    {
            //        await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeData();
            //    }
            //});

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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PhoneRecords}/{action=Index}/{id?}");
            });
        }
    }
}
