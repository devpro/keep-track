using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Withywoods.Dal.MongoDb;
using Withywoods.Dal.MongoDb.Serialization;

namespace KeepTrack.ConsoleApp
{
    public class AppConfiguration : IMongoDbConfiguration
    {
        #region Constructor & private fields

        private readonly IConfigurationRoot _configurationRoot;

        public AppConfiguration(IConfigurationRoot configurationRoot)
        {
            _configurationRoot = configurationRoot;
        }

        #endregion

        #region IMongoDbConfiguration properties

        public string ConnectionString => _configurationRoot.GetSection("KeepTrack_MongoDbConnectionString").Value;

        public string DatabaseName => "inventory";

        public List<string> SerializationConventions => new List<string> { ConventionValues.CamelCaseElementName, ConventionValues.EnumAsString,
            ConventionValues.IgnoreExtraElements, ConventionValues.IgnoreNullValues };

        #endregion
    }
}
