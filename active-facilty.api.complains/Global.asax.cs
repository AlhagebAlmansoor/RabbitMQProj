using active_facilty.domain.complains.handlers;
using active_facilty.domain.complains.models;
using api_facility.domain.complains.handlers;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using AutofacSerilogIntegration;

using EasyNetQ;
using EasyNetQ.DI;
using MediatR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace active_facilty.api.complains
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static Autofac.IContainer _container;

        private static IConnection connection;
        private static IModel channel;

        protected void Application_Start()
        {
           
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //var config = new HttpConfiguration();

            Log.Logger = new LoggerConfiguration()
                .Enrich.WithProperty("SourceContext", null)

                .WriteTo.RollingFile("c:\\Billings\\myapp.txt")
                .CreateLogger();

            var builder = new ContainerBuilder();
            builder.RegisterLogger(autowireProperties: true);

            builder.RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            builder.Register(c => new ComplainContext()).As<ComplainContext>().InstancePerRequest();

            builder.Register(c => RabbitHutch.CreateBus("host=10.10.10.7:5001;username=guest;password=guest")).As<IBus>().SingleInstance();

            //builder.Register(c => new QueueRabbitMQ(c.Resolve<IBus>())).As<IMessageBus>().InstancePerRequest();

            //builder.Register(c => new ComplainCommandHandler(c.Resolve<ComplainContext>())).As<IComplainCommandHandler>.InstancePerLifetimeScope();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.Register<SingleInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t =>
                {
                    object o;
                    return c.TryResolve(t, out o) ? o : null;
                };
            });

            builder.Register<MultiInstanceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => (IEnumerable<object>)c.Resolve(typeof(IEnumerable<>).MakeGenericType(t));
            });

            builder.RegisterAssemblyTypes(typeof(INotificationHandler<>).GetTypeInfo().Assembly).AsImplementedInterfaces();
            builder.RegisterType<ComplainCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<ComplainEventHandler>().AsImplementedInterfaces().InstancePerDependency();

            var container = builder.Build();

            _container = container;

            //SetupRabbitMqSubscriber();

            //Complainubscriber.Start();

            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((Autofac.IContainer)container); //Set the WebApi DependencyResolver
        }

        public void Application_End()
        {
            _container.Resolve<IBus>().Dispose();

        }

        
    }

    //public static class ComplainSubcriber
    //{
    //    public static void Start()
    //    {
    //        var bus = WebApiApplication._container.Resolve<IBus>();

    //        bus.Subscribe<UserCreated>("3", HandleUserCreatedForComplain);

    //    }

    //    private static void HandleUserCreatedForComplain(UserCreated obj)
    //    {
    //        var log = WebApiApplication._container.Resolve<ILogger>();
    //        log.Information("HandleUserCreatedForComplain");
    //    }
    //}
}
