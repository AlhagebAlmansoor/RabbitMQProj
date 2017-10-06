using active_facilty.domain.users.handlers;
using active_facilty.messages.users;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.domain.users.handlers
{
    public class UserEventHandler : INotificationHandler<UserCreated>
    {
        protected IBus bus;

        public UserEventHandler(IBus bus)
        {
            this.bus = bus;
        }

        public void Handle(UserCreated notification)
        {
            //Save ke read model

            this.bus.Publish(notification);
        }
    }
}
