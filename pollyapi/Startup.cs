using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Polly.Core.CustomEntities;
using Polly.Core.Interfaces;
using Polly.Core.Interfaces.ML;
using Polly.Core.Services.ML;
using Polly.Infrastructure.Data;
using Polly.Infrastructure.Extensions;
using Polly.Infrastructure.Filters;
using Polly.Infrastructure.Interfaces;
using Polly.Infrastructure.Repositories;
using Polly.Infrastructure.Repositories.ML;
using Polly.Infrastructure.Services;
using System;
using System.Reflection;
using System.Text;

// $$$$  TENANT
//https://michael-mckenna.com/multi-tenant-asp-dot-net-core-application-tenant-resolution
//https://www.codemag.com/Article/2101081/Building-Multi-Tenant-Applications-Using-ASP.NET-5
//https://dzone.com/articles/multi-tenant-api-based-on-swagger-entity-framework-1

//Para pasar el modelo de la base de datos al modelo de la aplicacion
//Comando para generar la base de datos:
//Scaffold-DbContext "Server=JORMANPC1;Database=polly;Integrated Security = true" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data
//add-migration "primera"
//Update-Database
//Remove-Migration

//CONVERTIR ESTRUCTURA DE TABLA A CLASE
//https://codverter.com/src/sqltoclass?prg=1&db=1&sample=1

namespace pollyapi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PollyContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            
            services.AddCors(opt => {
                opt.AddPolicy("CorsPolicy", policy => {
                    policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("*");
                });
            });
            
            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));
            services.Configure<PasswordOptions>(Configuration.GetSection("PasswordOptions"));

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddControllers(options =>
            {}).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
            })
            .ConfigureApiBehaviorOptions(options =>
            {});

            services.AddOptions(Configuration);
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IPasswordService, PasswordService>();
            services.AddSingleton<IUriService>(provider =>
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();
                var request = accesor.HttpContext.Request;
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());
                return new UriService(absoluteUri);
            });
         

            services.AddTransient<Iemergencia_resumenService, emergencia_resumenService>();
            services.AddTransient<Iemergencia_detalleService, emergencia_detalleService>();
            services.AddTransient<Iemergencia_detalleRepository, emergencia_detalleRepository>();
            services.AddTransient<Iemergencia_resumenRepository, emergencia_resumenRepository>();

            services.AddSwagger($"{Assembly.GetExecutingAssembly().GetName().Name}.xml");


            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Authentication:Issuer"],
                    ValidAudience = Configuration["Authentication:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:SecretKey"]))
                };

            });

            services.AddMvc(options =>
            {}).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<ErrorHandlingMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {                    
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Polly Api V1");
                    options.RoutePrefix = string.Empty;
                });
            }
            
            app.UseRouting();
            app.UseCors("CorsPolicy");

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedFor | Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.XForwardedProto
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
