using active_facilty.domain.users.models;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.domain.users.handlers
{
    //Command
    public class CreateCompany : INotification
    {
        public string Name { get; set; }
    }

    public class CompanyCommandHandler : INotificationHandler<CreateCompany>
    {
        protected UserContext ctx;
        protected IBus bus;

        public CompanyCommandHandler(UserContext ctx,IBus bus)
        {
            this.ctx = ctx;
            this.bus = bus;
        }

        public void Handle(CreateCompany cmd)
        {
            this.ctx.Companies.Add(new Company { Name = cmd.Name });
            this.ctx.SaveChanges();
        }
    }
}
