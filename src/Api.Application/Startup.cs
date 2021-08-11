using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Api.CrossCutting.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureService.ConfigureDependenciesService (services);
            ConfigureRepository.ConfigureDependenciesRepository (services);
            services.AddControllers();
            services
                .AddSwaggerGen(x =>
                {
                    x
                        .SwaggerDoc("v1",
                        new OpenApiInfo {
                            Version = "v1",
                            Title = "Curso de API com AspNetCore 3.1",
                            Description = "Arquitetura de Api DDD",
                            TermsOfService =
                                new Uri("http://www.mfrinfo.com.br"),
                            Contact =
                                new OpenApiContact {
                                    Name = "Davi França Maciel",
                                    Email = "davifrancamaciel@gmail.com",
                                    Url = new Uri("http://www.mfrinfo.com.br")
                                },
                            License =
                                new OpenApiLicense {
                                    Name = "Termo de licença de uso",
                                    Url = new Uri("http://www.mfrinfo.com.br")
                                }
                        });
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app
                .UseSwaggerUI(x =>
                {
                    x
                        .SwaggerEndpoint("/swagger/v1/swagger.json",
                        "Curso de API com AspNetCore 3.1");
                    x.RoutePrefix = string.Empty;
                });

            app.UseRouting();

            app.UseAuthorization();

            app
                .UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
        }
    }
}
