using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.HealthChecks;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Swashbuckle.AspNetCore.Swagger;
using WhiskyArchive.BuildingBlocks.EventBus;
using WhiskyArchive.BuildingBlocks.EventBus.Abstractions;
using WhiskyArchive.BuildingBlocks.EventBus.EventBusRabbitMQ;
using WhiskyArchive.BuildingBlocks.EventBus.IntegrationEventLogEF;
using WhiskyArchive.BuildingBlocks.EventBus.IntegrationEventLogEF.Services;
using WhiskyArchive.Services.WhiskyRecording.API.Application.IntegrationEvents;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.ActionResults;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.AutofacModules.ApplicationModule;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.AutofacModules.MediatorModule;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.Filters;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.Middlewares;
using WhiskyArchive.Services.WhiskyRecording.API.Infrasructure.Services;
using WhiskyArchive.Services.WhiskyRecording.Infrastructure;

namespace WhiskyRecording.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddCustomMvc()
               .AddHealthChecks(Configuration)
               .AddCustomDbContext(Configuration)
               .AddCustomSwagger(Configuration)
               .AddCustomIntegrations(Configuration)
               .AddCustomConfiguration(Configuration)
               .AddEventBus(Configuration)
               .AddCustomAuthentication(Configuration);

            //services.AddMediatR();


            var container = new ContainerBuilder();
            container.Populate(services);

            container.RegisterModule(new MediatorModule());
            container.RegisterModule(new ApplicationModule(Configuration["ConnectionString"]));

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var pathBase = Configuration["PATH_BASE"];
            if (!string.IsNullOrEmpty(pathBase))
            {
                //loggerFactory.CreateLogger<Startup>().LogDebug("Using PATH BASE '{pathBase}'", pathBase);
                app.UsePathBase(pathBase);
            }

#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
            app.Map("/liveness", lapp => lapp.Run(async ctx => ctx.Response.StatusCode = 200));
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

            app.UseCors("CorsPolicy");

            ConfigureAuth(app);
            
            app.UseMvcWithDefaultRoute();



            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
              
                //c.RoutePrefix = "api/docs";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Whisky Recording API V1");
                c.OAuthClientId("whiskyrecordingswaggerui");
                c.OAuthAppName("Whisky Recording Swagger UI");
               
            });


            ConfigureEventBus(app);

            WhiskyRecordingContextSeed.SimpleSeedAsync(app,env).Wait();
        }


        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();

            #region Integration Events
            /*eventBus.Subscribe
                <BacktestingDataCreatedIntegrationEvent,
                BacktestingDataCreatedIntegrationEventHandler>();
            eventBus.Subscribe
                <InvestmentSettledIntegrationEvent,
                InvestmentSettledIntegrationEventHandler>();
            eventBus.Subscribe
                <BacktestingInvestmentStartedIntegrationEvent,
                BacktestingInvestmentStartedIntegrationEventHandler>();
            eventBus.Subscribe
                <PaperInvestmentStartedIntegrationEvent,
                PaperInvestmentStartedIntegrationEventHandler>();
            eventBus.Subscribe
                <InvestmentCandleDataRequestedIntegrationEvent,
                InvestmentCandleDataRequestedIntegrationEventHandler>();
            eventBus.Subscribe
                <PaperTradeDataCreatedIntegrationEvent,
                PaperTradeDataCreatedIntegrationEventHandler>();
            eventBus.Subscribe
                <RoundtripTargetPriceHitIntegrationEvent,
                RoundtripTargetPriceHitIntegrationEventHandler>();
            eventBus.Subscribe
                <TargetPriceCandleDataCreatedIntegrationEvent,
                TargetPriceCandleDataCreatedIntegrationEventHandler>();
            eventBus.Subscribe
                <LiveInvestmentStartedIntegrationEvent,
                LiveInvestmentStartedIntegrationEventHandler>();*/
            #endregion
        }

        protected virtual void ConfigureAuth(IApplicationBuilder app)
        {
            if (Configuration.GetValue<bool>("UseLoadTest"))
            {
                app.UseMiddleware<ByPassAuthMiddleware>();
            }

            app.UseAuthentication();
        }

    }

    static class CustomExtensionsMethods
    {


        public static IServiceCollection AddCustomMvc(this IServiceCollection services)
        {
            
            // Add framework services.
            services.AddMvc(options =>
            {
                options.Filters.Add(typeof(HttpGlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
            .AddControllersAsServices();  //Injecting Controllers themselves thru DI
                                          //For further info see: http://docs.autofac.org/en/latest/integration/aspnetcore.html#controllers-as-services

            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            return services;
        }

        public static IServiceCollection AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddHealthChecks(checks =>
            {
                var minutes = 1;
                if (int.TryParse(configuration["HealthCheck:Timeout"], out var minutesParsed))
                {
                    minutes = minutesParsed;
                }
                checks.AddSqlCheck("whiskyarchive.whiskyrecording", configuration["ConnectionString"], TimeSpan.FromMinutes(minutes));
                checks.AddUrlCheck(configuration["WhiskyRecordingUrlHC"], TimeSpan.FromMinutes(minutes));
                //checks.AddUrlCheck(configuration["BasketUrlHC"], TimeSpan.Zero); //No cache for this HealthCheck, better just for demos 
                checks.AddUrlCheck(configuration["IdentityUrlHC"], TimeSpan.FromMinutes(minutes));
            });

            return services;
        }

        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEntityFrameworkSqlServer()
                   .AddDbContext<WhiskyRecordingContext>(options =>
                   {
                       options.UseSqlServer(configuration["ConnectionString"],
                           sqlServerOptionsAction: sqlOptions =>
                           {
                               sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                               sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                               sqlOptions.CommandTimeout(3 * 60);
                           });
                   },
                       ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
                   );

            /*services.AddDbContext<IntegrationEventLogContext>(options =>
            {
                options.UseSqlServer(configuration["ConnectionString"],
                                     sqlServerOptionsAction: sqlOptions =>
                                     {
                                         sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name);
                                             //Configuring Connection Resiliency: https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-resiliency 
                                             sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                                     });
            });*/

            return services;
        }

        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(options =>
            {
                options.DescribeAllEnumsAsStrings();
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Title = "Whisky Archive Whisky Recording API Document",
                    Version = "v1",
                    Description = "The Whisky Recording Service HTTP API",
                    TermsOfService = "Terms Of Service"
                });

                options.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/authorize",
                    TokenUrl = $"{configuration.GetValue<string>("IdentityUrlExternal")}/connect/token",
                    Scopes = new Dictionary<string, string>()
                    {
                        { "whiskyrecords", "Whisky Recording API" }
                    }
                });
                
                options.OperationFilter<AuthorizeCheckOperationFilter>();
                //options.OperationFilter<AuthorizationHeaderParameterOperationFilter>();

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            return services;
        }

        public static IServiceCollection AddCustomIntegrations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IIdentityService, IdentityService>();
            services.AddTransient<Func<DbConnection, IIntegrationEventLogService>>(
                sp => (DbConnection c) => new IntegrationEventLogService(c));

            services.AddTransient<IWhiskyRecordingIntegrationEventService, WhiskyRecordingIntegrationEventService>();

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                /*services.AddSingleton<IServiceBusPersisterConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultServiceBusPersisterConnection>>();

                    var serviceBusConnectionString = configuration["EventBusConnection"];
                    var serviceBusConnection = new ServiceBusConnectionStringBuilder(serviceBusConnectionString);

                    return new DefaultServiceBusPersisterConnection(serviceBusConnection, logger);
                });*/
            }
            else
            {
                services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();


                    var factory = new ConnectionFactory()
                    {
                        HostName = configuration["EventBusConnection"]
                    };

                    if (!string.IsNullOrEmpty(configuration["EventBusUserName"]))
                    {
                        factory.UserName = configuration["EventBusUserName"];
                    }
                    else
                    {
                        factory.UserName = "guest";
                    }

                    if (!string.IsNullOrEmpty(configuration["EventBusPassword"]))
                    {
                        factory.Password = configuration["EventBusPassword"];
                    }
                    else
                    {
                        factory.Password = "guest";
                    }

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new DefaultRabbitMQPersistentConnection(factory, logger, retryCount);
                });
            }

            return services;
        }

        public static IServiceCollection AddCustomConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions();

            //services.Configure<WhiskyRecoridngSettings>(configuration);

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var problemDetails = new ValidationProblemDetails(context.ModelState)
                    {
                        Instance = context.HttpContext.Request.Path,
                        Status = StatusCodes.Status400BadRequest,
                        Detail = "Please refer to the errors property for additional details."
                    };

                    return new BadRequestObjectResult(problemDetails)
                    {
                        ContentTypes = { "application/problem+json", "application/problem+xml" }
                    };
                };
            });

            /*services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });*/

            return services;
        }

        public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {

            var subscriptionClientName = configuration["SubscriptionClientName"];

            if (configuration.GetValue<bool>("AzureServiceBusEnabled"))
            {
                /*services.AddSingleton<IEventBus, EventBusServiceBus>(sp =>
                {
                    var serviceBusPersisterConnection = sp.GetRequiredService<IServiceBusPersisterConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusServiceBus>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusServiceBus(serviceBusPersisterConnection, logger,
                        eventBusSubcriptionsManager, subscriptionClientName, iLifetimeScope);
                });*/
            }
            else
            {
                services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                {
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    var retryCount = 5;
                    if (!string.IsNullOrEmpty(configuration["EventBusRetryCount"]))
                    {
                        retryCount = int.Parse(configuration["EventBusRetryCount"]);
                    }

                    return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, iLifetimeScope, eventBusSubcriptionsManager, subscriptionClientName, retryCount);
                });
            }

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            #region Integration Events Handlers
            /*services.AddTransient<BacktestingDataCreatedIntegrationEventHandler>();
            services.AddTransient<InvestmentSettledIntegrationEventHandler>();
            services.AddTransient<BacktestingInvestmentStartedIntegrationEventHandler>();
            services.AddTransient<PaperInvestmentStartedIntegrationEventHandler>();
            services.AddTransient<InvestmentCandleDataRequestedIntegrationEventHandler>();
            services.AddTransient<PaperTradeDataCreatedIntegrationEventHandler>();
            services.AddTransient<RoundtripTargetPriceHitIntegrationEventHandler>();
            services.AddTransient<TargetPriceCandleDataCreatedIntegrationEventHandler>();
            services.AddTransient<LiveInvestmentStartedIntegrationEventHandler>();*/
            #endregion

            return services;
        }

        public static IServiceCollection AddCustomAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            // prevent from mapping "sub" claim to nameidentifier.
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Remove("sub");

            var identityUrl = configuration.GetValue<string>("IdentityUrl");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            }).AddJwtBearer(options =>
            {
                options.Authority = identityUrl;
                options.RequireHttpsMetadata = false;
                options.Audience = "whiskyrecords";
            });

            return services;
        }
    }
}
