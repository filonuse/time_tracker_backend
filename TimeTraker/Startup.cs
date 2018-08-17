using BLL;
using BLL.Managers;
using Core.Interfaces.Managers;
using Core.Interfaces.Repositories;
using Core.Models.Models;
using DaL;
using DaL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using React.AspNet;
using Swashbuckle.AspNetCore.Swagger;
using System;
using TimeTraker.Options;
using TimeTraker.Services;

namespace TimeTraker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            // Set connection to database.
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            // Adds and configures the identity system for the specified User and Role types.
            services.AddIdentity<UserModel, IdentityRole>(opts =>
            {
                opts.User.RequireUniqueEmail = true; // unique email
                opts.Password.RequiredLength = 6;   // min lenght
                opts.Password.RequireNonAlphanumeric = false;   // alphanumeric symbols
                opts.Password.RequireLowercase = false; // lowercase symbols
                opts.Password.RequireUppercase = false; // UPPERcase symbols
                opts.Password.RequireDigit = false; // digits
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            // Add authentication services.
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = AuthOptions.ISSUER,
                        ValidAudience = AuthOptions.AUDIENCE,
                        IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
                    };
                });

            // Register the Swagger generator.
            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" }));
      
            #region Application services.
            // Repositories
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient(typeof(IFileRepository<FileModel>), typeof(FileRepository));

            // Managers
            services.AddTransient(typeof(IDataUpdateManager<>), typeof(DataUpdateManager<>));
            services.AddTransient(typeof(IEmployeesManager<UserModel>), typeof(EmployeesManager));
            services.AddTransient(typeof(IFileManager<FileModel>), typeof(FileManager));
            services.AddTransient(typeof(IGroupsManager<GroupModel, UserModel>), typeof(GroupsManager));

            // Other services
            services.AddTransient(typeof(JwtTokenService));
            #endregion

            // Add ReactJS services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddReact();

            services.AddMvc();
            return services.BuildServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            { 
                // Enable middleware to use BrowserLink.
                app.UseBrowserLink();

                // Enable middleware to use WebpackDev.
                app.UseWebpackDevMiddleware();
            }
            

            // Enabling middleware to serve swagger-ui.
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"); });

            // Enabling  middleware to use ReactJS.
            app.UseReact(config => { });

            // Enabling middleware to use StaticFiles
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseStatusCodePages();

            // Enabling middleware to use Authentication services.
            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
