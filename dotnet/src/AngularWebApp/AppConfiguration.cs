using Microsoft.Extensions.Configuration;

namespace KeepTrack.AngularWebApp
{
    public class AppConfiguration
    {
        #region Private fields & Constructor

        private readonly IConfiguration _configuration;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public AppConfiguration(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        #endregion
    }
}
