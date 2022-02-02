using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PhoneBook.Automapper;
using PhoneBook.Common.Models;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Repository;
using PhoneBook.Data;
using PhoneBook.Interfaces;
using PhoneBook.WebApiClient;
using System;
using System.Security.Claims;

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
            services.AddDatabase(Configuration);
            services.AddTransient<DbInitializer>();
            services.AddHttpClient<IRepository<PhoneRecordInfo>, WebRepository<PhoneRecordInfo>>((client) =>
            {
                var str = $"{Configuration["WebApi"]}{Configuration["PhoneRecordRepositoryAddress"]}";
                client.BaseAddress = new($"{Configuration["WebApi"]}{Configuration["PhoneRecordRepositoryAddress"]}");
            });            
            services.AddIdentity<User, ApplicationRole>()
                    .AddEntityFrameworkStores<PhoneBookDB>()
                    .AddDefaultTokenProviders()
                    .AddRoles<ApplicationRole>();
            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false;
                opt.Lockout.MaxFailedAccessAttempts = 10;
                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                opt.Lockout.AllowedForNewUsers = true;
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

            services.AddAutoMapper(typeof(Startup));
            services.AddMvc();
            services.AddControllersWithViews();
            
            
           
                       
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Если потребует создать БД с миграциями и тестовыми данными
            //using (var scope = app.ApplicationServices.CreateAsyncScope())
            //    await scope.ServiceProvider.GetRequiredService<DbInitializer>().InitializeData();
            
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
