using System;
using System.Collections.Generic;
using Api.CrossCutting.DependencyInjection;
using Api.CrossCutting.Mappings;
using Api.Data.Context;
using Api.Domain.Security;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace application
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment _env { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsEnvironment("Testing"))
            {
                Environment.SetEnvironmentVariable("DB_CONNECTION_MYSQL", "Persist Security Info=True;Server=localhost;Port=3306;Database=dbAPI_Integration;Uid=root;Pwd=86801qaz");
                Environment.SetEnvironmentVariable("DATABASE", "MYSQL");
                Environment.SetEnvironmentVariable("MIGRATION", "APLICAR");
                Environment.SetEnvironmentVariable("Audience", "ExemploAudience");
                Environment.SetEnvironmentVariable("Issuer", "ExemploIssuer");
                Environment.SetEnvironmentVariable("Seconds", "28800");
            }

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

            // var tokenConfigurations = new TokenConfigurations();
            // var config = Configuration.GetSection("TokenConfigurations");
            // new ConfigureFromConfigurationOptions<TokenConfigurations>(config)
            //     .Configure(tokenConfigurations);
            // services.AddSingleton (tokenConfigurations);
            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = signingConfigurations.Key;
                paramsValidation.ValidAudience = Environment.GetEnvironmentVariable("Audience");
                paramsValidation.ValidIssuer = Environment.GetEnvironmentVariable("Issuer");
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer",
                    new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build());
            });

            services.AddControllers();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Curso de API com AspNetCore 3.1/5.0",
                    Description = "Arquitetura de Api DDD",
                    TermsOfService = new Uri("http://www.mfrinfo.com.br"),
                    Contact = new OpenApiContact
                    {
                        Name = "Davi França Maciel",
                        Email = "davifrancamaciel@gmail.com",
                        Url = new Uri("http://www.mfrinfo.com.br")
                    },
                    License = new OpenApiLicense
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
                                    Reference =
                                        new OpenApiReference {
                                            Id = "Bearer",
                                            Type = ReferenceType.SecurityScheme
                                        }
                                },
                                new List<string>()
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
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Curso de API com AspNetCore 3.1/5.0");
                x.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            if (Environment.GetEnvironmentVariable("MIGRATION").ToUpper().Equals("APLICAR"))
            {
                using (var service = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
                {
                    using (var context = service.ServiceProvider.GetService<MyContext>())
                    {
                        context.Database.Migrate();
                    }
                }
            }
        }
    }
}
