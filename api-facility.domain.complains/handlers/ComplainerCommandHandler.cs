using active_facilty.domain.complains.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_facility.domain.complains.handlers
{
    public class CreateComplainer :INotification
    {
        public Complainer Complainer { get; set; }
    }

    public class ComplainerCommandHandler : INotificationHandler<CreateComplainer>
    {
        protected ComplainContext ctx;

        public ComplainerCommandHandler(ComplainContext ctx)
        {
            this.ctx = ctx;
        }

        public void Handle(CreateComplainer cmd)
        {
            ctx.Complainers.Add(cmd.Complainer);
            ctx.SaveChanges();
        }
    }
}
