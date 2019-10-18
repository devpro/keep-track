using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using CommandLine;
using KeepTrack.CarComponent.Domain;
using KeepTrack.CarComponent.Infrastructure.MongoDb.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Withywoods.Dal.MongoDb.DependencyInjection;
using Withywoods.Dal.MongoDb.MappingConverters;

namespace KeepTrack.ConsoleApp
{
    static class Program
    {
        #region Inner class

        public class CommandLineOptions
        {
            [Option('v', "verbose", Required = false, HelpText = "Set output to verbose messages.")]
            public bool IsVerbose { get; set; }

            [Option('a', "action", Required = true, HelpText = "Action (possible values: \"CarDemo\").")]
            public string Action { get; set; }

            [Option("id", Required = false, HelpText = "ID.")]
            public string Id { get; set; }
        }

        #endregion

        #region Entry point

        private async static Task Main(string[] args)
        {
            await Parser.Default.ParseArguments<CommandLineOptions>(args)
                .MapResult(
                    (CommandLineOptions opts) => RunOptionsAndReturnExitCode(opts),
                    errs => Task.FromResult(HandleParseError(errs))
                 );
        }

        private async static Task<int> RunOptionsAndReturnExitCode(CommandLineOptions opts)
        {
            LogVerbose(opts, "Create the service provider");

            var configuration = CreateConfiguration();

            using (var serviceProvider = CreateServiceProvider(configuration))
            {
                if (opts.Action == "CarDemo")
                {
                    var id = opts.Id;

                    LogVerbose(opts, "Query the car collection");

                    var carRepository = serviceProvider.GetService<ICarRepository>();
                    var car = await carRepository.FindOneAsync(id);

                    Console.WriteLine($"Car found: {car}");

                    LogVerbose(opts, "Query the car history collection");

                    var carHistoryRepository = serviceProvider.GetService<ICarHistoryRepository>();
                    var history = await carHistoryRepository.FindAllAsync(id);

                    Console.WriteLine($"Car history found: {history.Count}");
                }

                return 0;
            }
        }

        private static int HandleParseError(IEnumerable<Error> errs)
        {
            return -2;
        }

        #endregion

        #region Private helpers

        private static IConfigurationRoot CreateConfiguration()
        {
            return new ConfigurationBuilder()
                .SetBasePath(Path.GetDirectoryName(Assembly.GetEntryAssembly()?.Location))
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables()
                .Build();
        }

        private static ServiceProvider CreateServiceProvider(IConfigurationRoot configuration)
        {
            var serviceCollection = new ServiceCollection()
                .AddLogging(builder => { builder.AddConsole(); })
                .AddSingleton(configuration)
                .AddCarInfrastructureMongoDb()
                .AddMongoDbContext<AppConfiguration>();

            ConfigureAutoMapper(serviceCollection);

            return serviceCollection.BuildServiceProvider();
        }

        private static void ConfigureAutoMapper(IServiceCollection serviceCollection)
        {
            var mappingConfig = new MapperConfiguration(x => {
                x.AddProfile(new CarComponent.Infrastructure.MongoDb.MappingProfiles.CarMappingProfile());
                x.CreateMap<ObjectId, string>().ConvertUsing<ObjectIdToStringConverter>();
                x.CreateMap<string, ObjectId>().ConvertUsing<StringToObjectIdConverter>();
                x.AllowNullCollections = true;
            });
            var mapper = mappingConfig.CreateMapper();
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
            serviceCollection.AddSingleton(mapper);
        }

        private static void LogVerbose(CommandLineOptions opts, string message)
        {
            if (opts.IsVerbose)
            {
                Console.WriteLine(message);
            }
        }

        #endregion
    }
}
