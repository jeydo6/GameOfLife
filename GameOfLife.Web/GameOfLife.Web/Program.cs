using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System;
using System.Threading.Tasks;

namespace GameOfLife.Web
{
    public class Program
    {
        public static async Task Main(String[] args)
        {
            WebAssemblyHostBuilder builder = WebAssemblyHostBuilder
                .CreateDefault(args);

            builder
                .RootComponents
                .Add<App>("app");

            new Startup(builder.Configuration, builder.HostEnvironment)
                .ConfigureServices(builder.Services);

            await builder
                .Build()
                .RunAsync();
        }
    }
}
