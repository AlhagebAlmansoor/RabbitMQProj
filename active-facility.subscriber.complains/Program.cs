using active_facilty.domain.complains.handlers;
using active_facilty.domain.complains.models;
using active_facilty.messages.users;
using api_facility.domain.complains.handlers;
using Autofac;
using Autofac.Features.Variance;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.subscriber.complains
{
    class Program
    {
        public static Autofac.IContainer _container;

        static void Main(string[] args)
        {
            Config();
            using (var bus = RabbitHutch.CreateBus("host=10.10.10.7:5001;username=guest;password=guest"))
            {
                bus.Subscribe<UserCreated>("1",OnUserCreated);

                Console.WriteLine("Listem...");
                Console.ReadLine();
            }
        }

        private static void Config()
        {
            var builder = new ContainerBuilder();

            builder.RegisterSource(new ContravariantRegistrationSource());

            // mediator itself
            builder
              .RegisterType<Mediator>()
              .As<IMediator>()
              .InstancePerLifetimeScope();
            
            builder.Register(c => new ComplainContext()).As<ComplainContext>().InstancePerDependency();

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

            //builder.RegisterType<UserEventHandler>().AsImplementedInterfaces().InstancePerDependency();
            //builder.RegisterType<UserCommandHandler>().AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterType<ComplainCommandHandler>().AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterType<ComplainerCommandHandler>().AsImplementedInterfaces().InstancePerDependency();

            var container = builder.Build();

            _container = container;
        }

        private static void OnUserCreated(UserCreated obj)
        {
            var m = _container.Resolve<IMediator>();
            var createComplainer = new CreateComplainer
            {
                Complainer = new Complainer
                {
                    UserId = obj.UserId,
                    Name = obj.Name,
                    Email = obj.Email,
                    MobilePhone = obj.MobilePhone,
                    CompanyName = obj.CompanyName
                }
            };

            m.Publish(createComplainer);
            Console.WriteLine("Succes OnUserCreated");
        }
    }
}
