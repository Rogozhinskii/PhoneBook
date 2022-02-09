using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Common.Models;
using PhoneBook.Interfaces;
using PhoneBook.WebApiClient;
using MediatR;
using System;
using PhoneBook.CommandsAndQueries.Queries;

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
            
            
            services.AddHttpClient<IWebRepository<PhoneRecordInfo>, WebRepository<PhoneRecordInfo>>((client) =>
            {                
                client.BaseAddress = new($"{Configuration["WebApi"]}{Configuration["PhoneRecordRepositoryAddress"]}");                
            });
            services.AddHttpClient<IAuthentificationService, ApiAuthentification>((client) =>
            {
                client.BaseAddress = new($"{Configuration["WebApi"]}{Configuration["AccountManagementControllerAddress"]}");
            });
            services.AddHttpClient<IPermissionService, PermissionService>((client) =>
            {
                client.BaseAddress = new($"{Configuration["WebApi"]}{Configuration["PermissionControllerAddress"]}");
            });
            services.AddHttpClient<IUserManagementService, UserManagementService>((client) =>
            {
                client.BaseAddress = new($"{Configuration["WebApi"]}{Configuration["UserManagementControllerAddress"]}");
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });

           

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.Cookie.MaxAge = options.ExpireTimeSpan;
                options.LoginPath = "/Account/Login";
                options.LogoutPath = "/Account/Logout";
                options.SlidingExpiration = true;
            });
            services.AddMediatR(typeof(GetPageQuery).Assembly);
            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.AddControllersWithViews();
            
            
           
                       
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
                app.UseExceptionHandler("/Error");
            }
            app.UseStaticFiles();
            app.UseSession();
            app.UseRouting();
            
            app.UseAuthentication();
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
