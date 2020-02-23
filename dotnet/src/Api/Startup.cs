using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using KeepTrack.CarComponent.Infrastructure.MongoDb.DependencyInjection;
using KeepTrack.InventoryComponent.Infrastructure.MongoDb.DependencyInjection;
using KeepTrack.MovieComponent.Infrastructure.MongoDb.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
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
        #region Private fields & constructor

        private const string _CorsPolicyName = "CorsPolicyName";

        private readonly AppConfiguration _configuration;

        /// <summary>
        /// Create a new instance of <see cref="Startup"/>.
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            _configuration = new AppConfiguration(configuration);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Configure services.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(_configuration.ConfigurationRoot)
                .AddCarInfrastructureMongoDb()
                .AddInventoryInfrastructureMongoDb()
                .AddMovieInfrastructureMongoDb()
                .AddMongoDbContext<AppConfiguration>();

            ConfigureAutoMapper(services);

            ConfigureAuthentication(services, _configuration.ConfigurationRoot);

            if (_configuration.CorsAllowedOrigin != null)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(_CorsPolicyName,
                    builder =>
                    {
                        builder
                            .WithOrigins(_configuration.CorsAllowedOrigin.ToArray())
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
                });
            }

            services.AddControllers(opts =>
            {
                opts.Filters.Add<Filters.CustomExceptionFilterAttribute>();
            });

            ConfigureSwagger(services, _configuration.OpenApiInfo);
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

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseCors(_CorsPolicyName);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #endregion

        #region Private methods

        private static void ConfigureAutoMapper(IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(x =>
            {
                // Infrastructure MongoDB
                x.AddProfile(new CarComponent.Infrastructure.MongoDb.MappingProfiles.CarMappingProfile());
                x.AddProfile(new InventoryComponent.Infrastructure.MongoDb.MappingProfiles.InventoryMappingProfile());
                x.AddProfile(new MovieComponent.Infrastructure.MongoDb.MappingProfiles.MovieMappingProfile());
                x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
                x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
                // Api
                x.AddProfile(new MappingProfiles.GenericMappingProfile());
                // General
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            serviceCollection.AddSingleton(mapper);
        }

        private static void ConfigureAuthentication(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = configuration["Authentication:JwtBearer:Authority"];
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = configuration["Authentication:JwtBearer:TokenValidation:Issuer"],
                        ValidateAudience = true,
                        ValidAudience = configuration["Authentication:JwtBearer:TokenValidation:Audience"],
                        ValidateLifetime = true
                    };
                });
        }

        private static void ConfigureSwagger(IServiceCollection serviceCollection, OpenApiInfo openApiInfo)
        {
            serviceCollection.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(openApiInfo.Version,
                    new OpenApiInfo { Title = openApiInfo.Title, Version = openApiInfo.Version });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                        },
                        new[] { "readAccess", "writeAccess" }
                    }
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
            });
        }

        #endregion
    }
}
