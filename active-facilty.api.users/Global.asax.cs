using active_facility.domain.users.handlers;
using active_facilty.domain.users.handlers;
using active_facilty.domain.users.models;
using Autofac;
using Autofac.Features.Variance;
using Autofac.Integration.WebApi;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace active_facilty.api.users
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);


            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();

            builder.Register(c => new UserContext()).As<UserContext>().InstancePerRequest();

            builder.Register(c => RabbitHutch.CreateBus("host=192.168.0.104:5001;username=ryzam;password=pernu9801")).As<IBus>().SingleInstance();

            //builder.Register(c => new QueueRabbitMQ(c.Resolve<IBus>())).As<IMessageBus>().InstancePerRequest();

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

            //builder.RegisterAssemblyTypes(typeof(INotificationHandler<>).GetTypeInfo().Assembly).AsImplementedInterfaces();
            //builder.RegisterAssemblyTypes(typeof(IRequestHandler<,>).GetTypeInfo().Assembly).AsImplementedInterfaces();

            builder.RegisterType<UserEventHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<UserCommandHandler>().AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterType<CompanyCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<CompanyQueryHandler>().AsImplementedInterfaces().InstancePerDependency();

            var container = builder.Build();

            //config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver((Autofac.IContainer)container); //Set the WebApi DependencyResolver
        }

        protected void Application_End()
        {

        }
    }

    public static class UserSubscriber
    {
        public static void Start()
        {

        }
    }

}
