//using active_facility.messages;

using Autofac;
using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace active_facility.bus.queues
{
    public class MyLogger
    {
        public void Log(string component, string message)
        {
            Console.WriteLine("Component: {0} Message: {1} ", component, message);
        }
    }

    class Program
    {

        static Autofac.IContainer IContainer { get; set; }

        static void Main(string[] args)
        {
            //IocConfig();
            var bus = RabbitHutch.CreateBus("host=192.168.0.102:5001;username=ryzam;password=pernu9801");
                           bus.Subscribe<ComplainCreated>("1", HandleTextMessage);
            //bus.Subscribe<UserCreated>("2", HandleUserCreated);
            bus.Subscribe<UserCreated>("3", HandleUserCreatedForComplain);
            bus.Subscribe<UserCreated>("4", HandleUserForCrm);
            Console.WriteLine("Listening for messages. Hit <return> to quit.");
            Console.ReadLine();
        }

        private static void HandleUserForCrm(UserCreated obj)
        {
            Thread.Sleep(10000);
            Console.WriteLine("Receive event HandleUserForCrm - UserCreated ");
        }

        private static void HandleUserCreatedForComplain(UserCreated obj)
        {
            Console.WriteLine("Receive event HandleUserCreatedForComplain - UserCreated ");
        }

        private static void IocConfig()
        {

            var builder = new ContainerBuilder();

            builder.Register(c => new ComplainContext()).As<ComplainContext>().InstancePerDependency();
            builder.Register(c => new UserContext()).As<UserContext>().InstancePerDependency();



            var container = builder.Build();

            IContainer = container;
        }

        //private static void HandleUserCreated(UserCreated obj)
        //{
        //    Console.WriteLine("HandleUserCreated connecting...");
        //    using (var ctx = new ComplainContext())
        //    {
        //        //ComplainHandler h = new ComplainHandler(ctx);

        //        //h.Handle(obj);
        //    }

        //}

        static void HandleTextMessage(ComplainCreated m)
        {
            Console.WriteLine("Receive event ComplainCreated");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Got message: {0}", m.Title);
            Console.ResetColor();
        }
    }
}
