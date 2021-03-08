using System;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using KeepTrack.BlazorWebAssemblyApp.Authorization;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace KeepTrack.BlazorWebAssemblyApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped<ExternalAuthStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<ExternalAuthStateProvider>());

            // TODO: put in config, use HttClientFactory
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:5011") });

            await builder.Build().RunAsync();
        }
    }
}
