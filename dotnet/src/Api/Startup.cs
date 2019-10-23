using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using KeepTrack.CarComponent.Infrastructure.MongoDb.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.DependencyInjection;
using Withywoods.Dal.MongoDb.MappingConverters;

namespace KeepTrack.Api
{
    /// <summary>
    /// Application startup.
    /// </summary>
    public class Startup
    {
        private readonly AppConfiguration _configuration;

        /// <summary>
        /// Create a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = new AppConfiguration(configuration);
        }

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration.ConfigurationRoot)
                .AddCarInfrastructureMongoDb()
                .AddMongoDbContext<AppConfiguration>();

            ConfigureAutoMapper(services);

            // services
            //     .AddAuthentication(options =>
            //     {
            //         options.DefaultScheme = ...
            //         options.DefaultChallengeScheme = ...
            //     });

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(_configuration.OpenApiInfo.Version,
                    new OpenApiInfo { Title = _configuration.OpenApiInfo.Title, Version = _configuration.OpenApiInfo.Version });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        /// <summary>
        /// Configure the application pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint($"/swagger/{_configuration.OpenApiInfo.Version}/swagger.json", _configuration.OpenApiInfo.Title);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            // app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private static void ConfigureAutoMapper(IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                // Infrastructure MongoDB
                x.AddProfile(new CarComponent.Infrastructure.MongoDb.MappingProfiles.CarMappingProfile());
                x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
                x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
                // Api
                x.AddProfile(new MappingProfiles.CarMappingProfile());
                // General
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            serviceCollection.AddSingleton(mapper);
        }
    }
}
