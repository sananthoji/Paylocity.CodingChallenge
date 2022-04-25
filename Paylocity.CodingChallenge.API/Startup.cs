using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Paylocity.CodingChallenge.Core.Implementation;
using Paylocity.CodingChallenge.Core.Interfaces;
using Paylocity.CodingChallenge.Framework;
using Paylocity.CodingChallenge.Framework.ExceptionFilters;
using Paylocity.CodingChallenge.Framework.Middlewares;
using Paylocity.CodingChallenge.Infrastructure;
using Paylocity.CodingChallenge.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.CodingChallenge.API
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
            services.AddApplicationInsightsTelemetry();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Paylocity.CodingChallenge.API", Version = "v1" });
            });

            services.AddAuthentication(options =>
            {
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            //.AddJwtBearer(opt =>
            //{
            //    opt.Authority = "https://login.microsoftonline.com/8cf695c9-c697-4988-8d32-0431a46d8321";
            //    opt.Audience = "02e2421f-33ef-4404-bcc7-583d3e7c4115"; // Set this to the App ID URL for the web API, which you created when you registered the web API with Azure AD.
            //    opt.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = false
            //    };
            //    opt.Events = new JwtBearerEvents()
            //    {
            //        OnAuthenticationFailed = AuthenticationFailed,
            //        OnTokenValidated = TokenValidated,
            //        OnForbidden = ForbiddenContext1
            //    };
            //});
            .AddJwtBearer(options => Configuration.Bind("AzureAd", options));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(GlobalExceptionFilter));
            });

            services.AddCors();

            var connection = Configuration["Database:DbConnectionString"];
            services.AddDbContext<PaylocityCodingChallengeContext>(options => options.UseSqlServer(connection));
            services.AddScoped<IPaylocityCodingChallengeContext, PaylocityCodingChallengeContext>();
            services.AddScoped<IEmployeeProcessor, EmployeeProcessor>();

            services.AddScoped<IEmployeesService, EmployeesService>();
            services.AddScoped<IAnnualDeductionAmountService, AnnualDeductionAmountService>();
            services.AddScoped<INameDiscountService, NameDiscountService>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeDependentRepository, EmployeeDependentRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IDependentTypeRepository, DependentTypeRepository>();

            services.AddScoped<IPaylocityUserContext, PaylocityUserContext>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            logger.LogInformation(
                "Configuring for {Environment} environment",
                env.EnvironmentName);

            if (env.IsDevelopment())
            {
                IdentityModelEventSource.ShowPII = true;
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Paylocity.CodingChallenge.API v1"));
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UsePaylocityUserContext();

            app.UseCors(
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        // For debugging purpose.
        private Task AuthenticationFailed(AuthenticationFailedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg.Exception.Message}";
            //arg.Response.ContentLength = s.Length;
            //arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }

        private Task TokenValidated(TokenValidatedContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed:";
            //arg.Response.ContentLength = s.Length;
            //arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }

        private Task ForbiddenContext1(ForbiddenContext arg)
        {
            // For debugging purposes only!
            var s = $"AuthenticationFailed: {arg}";
            //arg.Response.ContentLength = s.Length;
            //arg.Response.Body.Write(Encoding.UTF8.GetBytes(s), 0, s.Length);
            return Task.FromResult(0);
        }
    }
}
