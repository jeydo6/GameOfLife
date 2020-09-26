using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using GameOfLife.Common.Behaviors;
using GameOfLife.Common.Filters;
using GameOfLife.Domain.Repositories;
using GameOfLife.Infrastructure.Repositories;
using MediatR;
using MediatR.Pipeline;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Reflection;

namespace GameOfLife.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers(options =>
                {
                    options.Filters.Add<ValidationExceptionFilter>();
                })
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.Formatting = Formatting.Indented;
                })
                .AddFluentValidation(options =>
                {
                    options.RegisterValidatorsFromAssemblyContaining<Application.AssemblyMarker>(
                        lifetime: ServiceLifetime.Singleton
                    );
                });

            services
                .AddSwaggerGen(options =>
                {
                    options.DescribeAllParametersInCamelCase();
                    options.SwaggerDoc("main", new OpenApiInfo
                    {
                        Version = "v0.1.0",
                        Title = "Game of Life",
                        Description = "A simple implementation of the Game of Life - cellular automaton, devised by J.H. Conway in 1970",
                        Contact = new OpenApiContact
                        {
                            Name = "Vladimir Deryagin",
                            Email = "Deryagin.Valdemar@yandex.ru"
                        },
                        License = new OpenApiLicense
                        {
                            Name = "Use under MIT"
                        }
                    });
                    options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please insert JWT with Bearer into field",
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey
                    });

                    String xmlFile;
                    
                    xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));

                    xmlFile = $"{Assembly.GetAssembly(typeof(Application.AssemblyMarker)).GetName().Name}.xml";
                    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFile));
                });

            services
                .AddMediatR(typeof(Application.AssemblyMarker))
                .AddAutoMapper(typeof(Application.AssemblyMarker));

            services
                .AddTransient(typeof(IRequestPreProcessor<>), typeof(ValidatorPreProcessor<>));

            services
                .AddSingleton<IFieldsRepository, FieldsRepository>()
                .AddSingleton<IBehaviorsRepository, BehaviorsRepository>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseStaticFiles();
                app.UseDeveloperExceptionPage();

                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/main/swagger.json", "Game of Life API");
                });
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
