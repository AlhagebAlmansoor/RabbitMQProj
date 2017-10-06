using active_facilty.domain.complains.handlers;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_facility.domain.complains.handlers
{
    public class ComplainEventHandler
    {
        protected IBus bus;

        public ComplainEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        //public void Handle(ComplainCreated @event)
        //{
        //    this.bus.Publish(@event);
        //}
    }
}
