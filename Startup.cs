using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using McGillWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using McGillWebAPI.Options;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.EntityFrameworkCore;
using McGillWebAPI.Model;

namespace McGillWebAPI
{
    public class Startup
    {
        private IHostingEnvironment _env; 
         #warning You need to set the environment variable SECRET_KEY     
        private static readonly string SecretKey = Environment.GetEnvironmentVariable("SECRET_KEY");  //TODO get this from the environment instead of putting in Git
        private readonly SymmetricSecurityKey _signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));

        public Startup(IHostingEnvironment env)
        {
            _env = env;
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Database connection
            // var connection = @"Server=SPIDERMAN\mcgillweb;Database=AirSilence;Trusted_Connection=True;";
            // services.AddDbContext<AirSilenceContext>(options => options.UseSqlServer(connection));

            // Allow localhost to Cors
            services.AddCors( options => {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials() );
            });

            // Add framework services.
            services.AddOptions();
            
            // The following is needed to retreive sectionsA-F of onlineapp
            // otherwise, there is a loop problem since sections contain the employmentAppId
            services.AddMvc().AddJsonOptions(options => {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            // services.AddMvc().AddJsonOptions(options => {
            //     options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            // });
            // Tell injector about IMailService and MailService
            // Use AddScoped for a single instance within a request
            // use _env to perform logic for _env.IsDevelopment or IsProduction etc.
            
            services.AddSingleton(Configuration);
            services.AddScoped<IMailService, MailService>();

            // File size upload limit
            services.Configure<FormOptions>( options => {
                options.MultipartBodyLengthLimit = 2147483648;  // 2GB
            });

            // service.AddDbContext when working with databases

            // Make authentication compulsory across the board (i.e. shut
            // down EVERYTHING unless explicitly opened up).
            // Part I](https://goblincoding.com/2016/07/03/issuing-and-authenticating-jwt-tokens-in-asp-net-core-webapi-part-i/) addresses how JSON Web Token can be configured and issued.
            // [Part II](https://goblincoding.com/2016/07/07/issuing-and-authenticating-jwt-tokens-in-asp-net-core-webapi-part-ii/) covers the authorisation aspects focusing on user claims using ASP.NET Core MVC’s policy features.
            //[Bonus] https://goblincoding.com/2016/07/24/asp-net-core-policy-based-authorisation-using-json-web-tokens/

            services.AddMvc( config => 
            {
                var policy = new AuthorizationPolicyBuilder()
                         .RequireAuthenticatedUser()
                         .Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });

            // Use policy auth.
            services.AddAuthorization(options =>
            {
                // Define a policy/API marked with "DisneyUser" that requires ApplicationUser with claims/roles of "DisneyCharacter" AND "IAmMickey"
                options.AddPolicy("DisneyUser", policy => policy.RequireClaim("DisneyCharacter", "IAmMickey"));
            });

            // Get options from app settings
            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));

            // Configure JwtIssuerOptions
            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            // Configure AwtBearer for authorization
            // app.UseJwtBearerAuthentication(
            // configure options here, read stuff from Configuration, etc.   );

            // Default {api/<name>/<param1>}  Where name is <name>Controller.cs by convention
            // i.e. ContactUsController.cs is api/contactus/1

            var jwtAppSettingOptions = Configuration.GetSection(nameof(JwtIssuerOptions));
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = _signingKey,

                RequireExpirationTime = true,
                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };

            app.UseJwtBearerAuthentication(new JwtBearerOptions
            {
                AutomaticAuthenticate = true,
                AutomaticChallenge = true,
                TokenValidationParameters = tokenValidationParameters
            });             

            app.UseCors("CorsPolicy");
            app.UseMvc();
        }
    }
}
