using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace KeepTrack.Api
{
    /// <summary>
    /// Application program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Starting point.
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        /// <summary>
        /// Create web application web host builder.
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
        }
    }
}
