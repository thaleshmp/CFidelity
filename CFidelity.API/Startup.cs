using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Http;
using CFidelity.API.Domain.Logging;
using CFidelity.API.Settings;
using CFidelity.API.Repository.Interface;
using CFidelity.API.Repository;

namespace CFidelity.API
{
    public class Startup
    {
        const string ISO_8861_DateTimeFormat = "yyyy-MM-ddTHH:mm:ssZ";

        public IConfigurationRoot Configuration { get; }

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables()
                .Build();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration);

            OptionsConfigurationServiceCollectionExtensions.Configure<MongoDBSettings>(services, Configuration.GetSection("MongoDBSettings"));

            services.AddScoped<IOfertaRepository, OfertaRepository>();

            services.AddCors();
            services.AddMvc(options =>
            {

            }).AddJsonOptions(options =>
            {
                options.SerializerSettings.DateFormatString = ISO_8861_DateTimeFormat;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                // options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
            });

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(cors =>
            {
                cors.AllowAnyHeader();
                cors.AllowAnyMethod();
                cors.AllowAnyOrigin();
            });

            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();

            app.UseWelcomePage("/");
        }
    }
}
