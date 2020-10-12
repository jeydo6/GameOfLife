using GameOfLife.Domain.Services;
using GameOfLife.Infrastructure.Configs;
using GameOfLife.Infrastructure.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Text.Json;

namespace GameOfLife.Web
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        private IWebAssemblyHostEnvironment HostEnvironment { get; }

        public Startup(IConfiguration configuration, IWebAssemblyHostEnvironment hostEnvironment)
        {
            Configuration = configuration;
            HostEnvironment = hostEnvironment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddOptions();

            services
                .AddHttpClient<IFieldsService, FieldsService>((sp, httpClient) =>
                {
                    var config = sp.GetRequiredService<GameOfLifeConfig>();

                    httpClient.BaseAddress = new Uri(config.Address);
                });

            services
                .AddAuthorizationCore();

            //services
            //    .AddScoped<IFieldsService, FieldsService>()

            services
                .AddSingleton(
                    Configuration
                        .GetSection("Resources:GameOfLife.API")
                        .Get<GameOfLifeConfig>()
                );

            services
                .AddSingleton(new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }

}
