using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Api.CrossCutting.DependencyInjection;
using Api.Domain.Security;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authorization;
using Api.CrossCutting.Mappings;
using AutoMapper;

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
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesRepository(services);

            var configMapper = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new DtoToModelProfile());
                cfg.AddProfile(new EntityToDtoProfile());
                cfg.AddProfile(new ModelToEntityProfile());
            });

            IMapper mapper = configMapper.CreateMapper();
            services.AddSingleton(mapper);

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            var tokenConfigurations = new TokenConfigurations();
            var config = Configuration.GetSection("TokenConfigurations");
            new ConfigureFromConfigurationOptions<TokenConfigurations>(config)
                .Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services
                .AddAuthentication(authOptions =>
                {
                    authOptions.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                    authOptions.DefaultChallengeScheme =
                        JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(bearerOptions =>
                {
                    var paramsValidation = bearerOptions.TokenValidationParameters;
                    paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                    paramsValidation.ValidAudience = tokenConfigurations.Audience;
                    paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                    paramsValidation.ValidateIssuerSigningKey = true;
                    paramsValidation.ValidateLifetime = true;
                    paramsValidation.ClockSkew = TimeSpan.Zero;
                });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });

            services.AddControllers();
            services
                .AddSwaggerGen(x =>
                {
                    x
                        .SwaggerDoc("v1",
                        new OpenApiInfo
                        {
                            Version = "v1",
                            Title = "Curso de API com AspNetCore 3.1",
                            Description = "Arquitetura de Api DDD",
                            TermsOfService =
                                new Uri("http://www.mfrinfo.com.br"),
                            Contact =
                                new OpenApiContact
                                {
                                    Name = "Davi França Maciel",
                                    Email = "davifrancamaciel@gmail.com",
                                    Url = new Uri("http://www.mfrinfo.com.br")
                                },
                            License =
                                new OpenApiLicense
                                {
                                    Name = "Termo de licença de uso",
                                    Url = new Uri("http://www.mfrinfo.com.br")
                                }
                        });
                    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Entre com o Token JWT",
                        Name = "Authorization",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.ApiKey
                    });

                    x.AddSecurityRequirement(new OpenApiSecurityRequirement {
                    {
                        new OpenApiSecurityScheme {
                        Reference= new OpenApiReference{
                            Id = "Bearer",
                            Type =ReferenceType.SecurityScheme
                        }
                    }, new List<string>()
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
