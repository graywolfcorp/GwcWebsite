using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Gwc.Common.Interfaces.Services;
using GWC.Web.Configuration;
using GWC.Web.DataAccess;
using GWC.Web.Dtos;
using GWC.Web.Services.AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

#pragma warning disable 1591
namespace GWC.Web.Api
{
    public class Startup
    {
        private readonly IConfigurationRoot _configuration;
        private readonly IHostingEnvironment _environment;
        private readonly WeblogConfigurationDto _webConfiguration = new WeblogConfigurationDto();

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables();

            if (environment.IsDevelopment())
            {
                configurationBuilder.AddUserSecrets<Startup>();
            }

            _configuration = configurationBuilder.Build();
            _configuration.GetSection("AppSettings").Bind(_webConfiguration);
            _environment = environment;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(
            IApplicationBuilder app, 
            IHostingEnvironment env, 
            ILoggerFactory loggerFactory,
            GwcDbContext dbContext)
        {
            app.UseCors("SiteCorsPolicy");
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
            app.UseMvc();
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "wwwroot";

                if (env.IsDevelopment())
                {
                    //spa.UseAngularCliServer(npmScript: "start-dev");
                }
            });

            GraywolfDbInitializer.Initialize(dbContext);
        }       

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            var corsBuilder = new CorsPolicyBuilder();
            corsBuilder.AllowAnyHeader();
            corsBuilder.AllowAnyMethod();
            corsBuilder.AllowAnyOrigin(); // For anyone access.
            //corsBuilder.WithOrigins("http://localhost:56573"); // for a specific url. Don't add a forward slash on the end!
            corsBuilder.AllowCredentials();

            services.AddCors(options =>
            {
                options.AddPolicy("SiteCorsPolicy", corsBuilder.Build());
            });

            services.AddDbContext<GwcDbContext>(cfg =>
            {
                cfg.UseSqlServer(_webConfiguration.ConnectionString);
            });

           services.AddSingleton<IConfiguration>(_configuration);

            services.AddMvc(opt =>
            {
                opt.InputFormatters.Add(new ContentfulInputFormatter());

                if (_environment.IsProduction())
                {
                    //opt.Filters.Add(new RequireHttpsAttribute());
                }
            })
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "Gray Wolf Corporation API",
                    Description = "Details on the APIs available at graywolfcorp.com",
                    Contact = new Contact
                    {
                        Name = "Park Espenschade",
                        Email = "park(at)graywolfcorp.com",
                        Url = "http://graywolfcorp.com"
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var basePath = AppContext.BaseDirectory;
                var xmlPath = Path.Combine(basePath, "GWC.Web.Api.xml");
                c.IncludeXmlComments(xmlPath);
            });

            services.AddSpaStaticFiles(c =>
            {
                c.RootPath = "wwwroot";
            });

            services.AddAutoMapper(x => x.AddProfile<GwcModelDtoProfile>());

            //var key = Encoding.ASCII.GetBytes(_webConfiguration.secret);

            //services.AddAuthentication(x =>
            //{
            //    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            //    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            //})
            //.AddJwtBearer(x =>
            //{
            //    x.RequireHttpsMetadata = false;
            //    x.SaveToken = true;
            //    x.Audience = _webConfiguration.AzureAd.ClientId;
            //    x.Authority = String.Format(_webConfiguration.AzureAd.AadInstance, _webConfiguration.AzureAd.Tenant);
            //    x.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        AuthenticationType = "Bearer",
            //        ValidateIssuerSigningKey = true,
            //        IssuerSigningKey = new SymmetricSecurityKey(key),
            //        ValidateIssuer = false,
            //        ValidateAudience = false
            //    };
            //});

            //needed for NLog.Web
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            var developmentMode = _webConfiguration.DevelopmentMode.ToLower() == "true";
            var logLevel = _webConfiguration.LogLevel;
            //var logFolder = _configuration["AppSettings:logFolder"];

            var containerBuilder = new ContainerBuilder();
            AutofacConfig.Register(containerBuilder, developmentMode);
            containerBuilder.Populate(services);
            var applicationContainer = containerBuilder.Build();
            var serviceProvider = new AutofacServiceProvider(applicationContainer);

            var _logManagerService = serviceProvider.GetService<ILogManagerService>();
            var _loggingService = serviceProvider.GetService<ILoggingService>();

            var connectionString = _webConfiguration.ConnectionString;

            _loggingService.AddDatabaseTarget(connectionString, logLevel, _logManagerService.DefaultDbLogger);
            //_loggingService.AddFileTarget(logFolder.Add("nLogTesting.txt"), logLevel, _logManagerService.DefaultFileLogger);

            return serviceProvider;
        }

        // Handle sign-in errors differently than generic errors.
        private Task OnAuthenticationFailed(RemoteFailureContext context)
        {
            context.HandleResponse();
            //context.Response.Redirect("/Home/Error?message=" + context.Failure.Message);
            return Task.FromResult(0);
        }
    }
}
#pragma warning restore 1591