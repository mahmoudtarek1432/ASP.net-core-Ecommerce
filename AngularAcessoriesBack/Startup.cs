using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngularAcessoriesBack.Data;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Cors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AngularAcessoriesBack.Services;
using AngularAcessoriesBack.Models;

namespace AngularAcessoriesBack
{
    public class Startup
    {
        private readonly string MyAllowSpecificOrigins = "http://localhost:4200";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DbContexts>(opt => opt.UseSqlServer("Server = localhost; Initial Catalog = AngularAPIDb; User ID = AngularAPI; Password = Gundam00;"));

            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentity<CustomIdentityUser, IdentityRole>(options =>
             {
                 options.Password.RequireDigit = true;
                 options.Password.RequiredLength = 8;
                 options.Password.RequireLowercase = true;
                 options.Password.RequireUppercase = true;
                 options.Password.RequireNonAlphanumeric = false;
             }).AddEntityFrameworkStores<DbContexts>()
             .AddDefaultTokenProviders();

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = Configuration["AuthSettings:Audience"],
                    ValidIssuer = Configuration["AuthSettings:Issuer"],
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["AuthSettings:Key"]))

                };
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddAuthorization();

            services.AddScoped<IProductRepo, SqlProductRepo>();
            services.AddScoped<IReviewRepo, SqlReviewRepo>();
            services.AddScoped<IBannerRepo, SqlBannerRepo>();
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<ICartRepo, SqlCartRepo>();
            services.AddScoped<IOrderRepo, SqlOrderRepo>();
            services.AddTransient<IEMailService, EMailService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            app.UseCors(options => options.WithOrigins(MyAllowSpecificOrigins).AllowAnyMethod().AllowAnyHeader().AllowCredentials());

            //before app.UseAuthentication(); as it is used to append the jwt cookie to the header
            app.UseMiddleware<JWTInHeaderMiddleware>(); 

            //app.useauth must be before usemvc
            app.UseAuthentication();
            
            app.UseMvc();



        }
    }
}

//next is.. the identity framework users