﻿using Withywoods.Configuration;
using Withywoods.Dal.MongoDb;
using Withywoods.Dal.MongoDb.Serialization;

namespace KeepTrack.Api
{
    /// <summary>
    /// Web application configuration.
    /// This class implements the interface from the libraries that are used in the application.
    /// </summary>
    public class AppConfiguration : IMongoDbConfiguration
    {
        /// <summary>
        /// Create a new instance of <see cref="AppConfiguration"/>
        /// </summary>
        /// <param name="configurationRoot"></param>
        public AppConfiguration(IConfiguration configurationRoot)
        {
            ConfigurationRoot = configurationRoot;
        }

        /// <summary>
        /// Configuration root.
        /// </summary>
        public IConfiguration ConfigurationRoot { get; set; }

        /// <summary>
        /// MongoDB connection string => secret!
        /// This is really a sensitive information so better defined as an environment variable.
        /// </summary>
        string IMongoDbConfiguration.ConnectionString => ConfigurationRoot.TryGetSection("Infrastructure:MongoDB:ConnectionString").Value;

        /// <summary>
        /// MongoDB collection name.
        /// </summary>
        string IMongoDbConfiguration.DatabaseName => ConfigurationRoot.TryGetSection("Infrastructure:MongoDB:DatabaseName").Value;

        /// <summary>
        /// MongoDB serialization conventions.
        /// </summary>
        List<string> IMongoDbConfiguration.SerializationConventions =>
            new()
            {
                ConventionValues.CamelCaseElementName,
                ConventionValues.EnumAsString,
                ConventionValues.IgnoreExtraElements,
                ConventionValues.IgnoreNullValues
            };

        /// <summary>
        /// Open API information.
        /// </summary>
        public OpenApiInfo OpenApiInfo =>
            new()
            {
                Title = "Keep Track API",
                Version = "1.0"
            };

        /// <summary>
        /// Allowed Origin URL for Cross-Origin Requests (CORS)
        /// </summary>
        /// <remarks>
        /// See https://docs.microsoft.com/en-us/aspnet/core/security/cors?view=aspnetcore-3.1
        /// </remarks>
        public List<string> CorsAllowedOrigin => ConfigurationRoot.TryGetSection("AllowedOrigins").Get<List<string>>();
    }
}
