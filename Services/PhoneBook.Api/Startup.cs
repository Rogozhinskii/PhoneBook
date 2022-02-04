using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhoneBook.Common.Models;
using PhoneBook.DAL;
using PhoneBook.DAL.Context;
using PhoneBook.DAL.Repository;
using PhoneBook.Interfaces;

namespace PhoneBook.Api
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
            services.AddDatabase(Configuration);
            services.AddScoped(typeof(IRepository<>), typeof(DbRepository<>));
            services.AddIdentity<User, ApplicationRole>()
                    .AddEntityFrameworkStores<PhoneBookDB>()
                    .AddDefaultTokenProviders()
                    .AddRoles<ApplicationRole>();
            services.AddCors();            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook.Api", Version = "v1" });
            });
            
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PhoneBook.Api v1"));
            }

            app.UseRouting();
            app.UseCors(options =>
            {
                options.AllowAnyOrigin();                
                options.WithOrigins(Configuration["ClientHost"]);
            });
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
