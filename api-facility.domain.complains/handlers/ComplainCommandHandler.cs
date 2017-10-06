using active_facilty.domain.complains.models;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace active_facilty.domain.complains.handlers
{
    public class CreateComplain : INotification
    {
        public string Title { get; set; }
    }

    //public class ComplainCreated : INotification
    //{
    //    public string Title { get; set; }
    //}

    public class ComplainCommandHandler : INotificationHandler<CreateComplain>
    {
        protected ComplainContext ctx;

        protected IMediator mediator;

        public ComplainCommandHandler(IMediator mediator,ComplainContext ctx)
        {
            this.ctx = ctx;
            this.mediator = mediator;
        }

        public void Handle(CreateComplain message)
        {
            Console.WriteLine("Create complain");

            ctx.Complains.Add(new Complain { TicketNumber = "Mediatr01" });

            ctx.SaveChanges();

            ctx.Dispose();

            //this.mediator.Publish<ComplainCreated>(new ComplainCreated {});
        }
    }

    
}