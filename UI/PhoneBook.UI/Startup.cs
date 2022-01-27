using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Automapper;
using PhoneBook.DAL.Repository;
using PhoneBook.Data;
using PhoneBook.Interfaces;
using System;
using System.Threading.Tasks;

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
        public void ConfigureServices(IServiceCollection services) {
            services.AddDatabase(Configuration).AddAutoMapper(typeof(Startup))
                   .AddScoped(typeof(IRepository<>), typeof(DbRepository<>))
                   .AddScoped(typeof(IMappedRepository<,>), typeof(MappedRepository<,>))
                   .AddScoped(typeof(DbInitializer))
                   .AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=PhoneRecords}/{action=Index}");
            });
        }
    }
}
