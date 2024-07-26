using System.Reflection;
using System.Text.Json.Serialization;
using AutoMapper;
using FluentValidation.AspNetCore;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Para.Api.Middleware;
using Para.Api.Service;
using Para.Bussiness;
using Para.Bussiness.Cqrs;
using Para.Data.Context;
using Para.Data.UnitOfWork;
using Para.Data.CustomerReportRepository;
using Autofac;
using Autofac.Extensions.DependencyInjection;

namespace Para.Api
{
    public class Startup
    {
        public IConfiguration Configuration;

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Para.Api", Version = "v1" });
            });

            var connectionStringSql = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<ParaDbContext>(options => options.UseSqlServer(connectionStringSql));

            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperConfig());
            });
            services.AddSingleton(config.CreateMapper());

            services.AddMediatR(typeof(CreateCustomerCommand).GetTypeInfo().Assembly);
            services.AddMediatR(typeof(CreateCustomerDetailCommand).GetTypeInfo().Assembly);

            services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
            services.AddValidatorsFromAssemblyContaining<Startup>();
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            // Autofac modüllerini ve bağımlılıkları burada kaydedin
            builder.RegisterType<CustomerReportRepository>().As<ICustomerReportRepository>().InstancePerLifetimeScope();
            builder.RegisterType<UnitOfWork>().As<IUnitOfWork>().InstancePerLifetimeScope();
            builder.RegisterType<CustomService1>().InstancePerDependency();
            builder.RegisterType<CustomService2>().InstancePerLifetimeScope();
            builder.RegisterType<CustomService3>().SingleInstance();

            // Diğer servislerinizi de burada kaydedin
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Para.Api v1"));
            }

            app.UseMiddleware<HeartbeatMiddleware>();
            app.UseMiddleware<ErrorHandlerMiddleware>();
            app.UseMiddleware<RequestResponseLoggingMiddleware>();

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Use((context, next) =>
            {
                if (!string.IsNullOrEmpty(context.Request.Path) && context.Request.Path.Value.Contains("favicon"))
                {
                    return next();
                }

                var service1 = context.RequestServices.GetRequiredService<CustomService1>();
                var service2 = context.RequestServices.GetRequiredService<CustomService2>();
                var service3 = context.RequestServices.GetRequiredService<CustomService3>();

                service1.Counter++;
                service2.Counter++;
                service3.Counter++;

                return next();
            });

            app.Run(async context =>
            {
                var service1 = context.RequestServices.GetRequiredService<CustomService1>();
                var service2 = context.RequestServices.GetRequiredService<CustomService2>();
                var service3 = context.RequestServices.GetRequiredService<CustomService3>();

                if (!string.IsNullOrEmpty(context.Request.Path) && !context.Request.Path.Value.Contains("favicon"))
                {
                    service1.Counter++;
                    service2.Counter++;
                    service3.Counter++;
                }

                await context.Response.WriteAsync($"Service1 : {service1.Counter}\n");
                await context.Response.WriteAsync($"Service2 : {service2.Counter}\n");
                await context.Response.WriteAsync($"Service3 : {service3.Counter}\n");
            });
        }
    }
}
