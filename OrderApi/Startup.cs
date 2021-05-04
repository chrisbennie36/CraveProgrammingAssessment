using Domains.Orders.Interfaces;
using Domains.Orders.Repositories;
using Domains.Products.Interfaces;
using Domains.Products.Repositories;
using Infrastructure.EventPublisher;
using Infrastructure.EventPublisher.Interfaces;
using Infrastructure.RepositoryConfiguration;
using Infrastructure.Sql;
using Infrastructure.Sql.Interfaces;
using Infrastructure.TableStorage;
using Infrastructure.TableStorage.Interfaces;
using Logging;
using Logging.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace OrderApi
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
            services.AddControllers();

            services.AddSwaggerExamples();

            services.AddSwaggerGen(c => 
            {
                c.ExampleFilters();
            });

            services.Configure<LoggerConfiguration>(Configuration.GetSection("LoggerConfiguration"));
            services.Configure<ConnectionStringsConfiguration>(Configuration.GetSection("ConnectionStrings"));

            services.AddTransient<ILoggerConfiguration, LoggerConfiguration>();
            services.AddTransient<ILogger>(x => new Logger(x.GetService<IOptions<LoggerConfiguration>>().Value));

            services.AddTransient<ISqlRepository, SqlRepository>();
            services.AddTransient<ITableStorageRepository, TableStorageRepository>();

            services.AddTransient<IOrderCommandRepository, OrderCommandRepository>();
            services.AddTransient<IOrderQueryRepository, OrderQueryRepository>();

            services.AddTransient<IProductCommandRepository, ProductCommandRepository>();
            services.AddTransient<IProductQueryRepository, ProductQueryRepository>();

            services.AddSingleton(typeof(IEventPublisher<INotification>), typeof(EventPublisher));
            services.AddSingleton(typeof(IQueue<INotification>), typeof(Infrastructure.EventPublisher.Queue<INotification>));

            var managedAssemblies = GetHandlerAssemblies();

            services.AddMediatR(managedAssemblies.ToArray());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("v1/swagger.json", "Ordering API");
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var logger = app.ApplicationServices.GetService<ILogger>();
            var mediator = app.ApplicationServices.GetService<IMediator>();
            var queue = app.ApplicationServices.GetService<IQueue<INotification>>();

            var notificationEventDispatcher = new EventQueueDispatcher(mediator, queue, logger);
            notificationEventDispatcher.Start();

            logger.Info("Application started");
        }

        public List<Assembly> GetHandlerAssemblies()
        {
            var assemblies = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll")
                                .Select(x => Assembly.Load(AssemblyName.GetAssemblyName(x)));

            return assemblies.Where(
                a => a.FullName.Contains("Domains.Orders") || 
                a.FullName.Contains("Domains.Products")).ToList();
        }
    }
}
