using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using EasyNetQ;
using active_facilty.domain.users.models;
using active_facilty.messages.users;

namespace active_facilty.domain.users.handlers
{
    public class CreateUser : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public Guid CompanyId { get; set; }
        public string CompanyName { get; set; }
    }


    //Handler
    public class UserCommandHandler : INotificationHandler<CreateUser>
    {
       
        protected UserContext ctx;

        protected IMediator mediator;

        protected IBus bus;

        public UserCommandHandler(IMediator mediator, UserContext ctx,IBus bus)
        {

            this.ctx = ctx;
            this.mediator = mediator;
            this.bus = bus;
        }

        public void Handle(CreateUser cmd)
        {
            //Save kedalam DB
            var company = ctx.Companies.Where(c => c.Id == cmd.CompanyId).FirstOrDefault();

            var user = new User { CompanyId = cmd.CompanyId, Name = cmd.Name, Email = cmd.Email, MobilePhone = cmd.MobilePhone };

            ctx.Users.Add(user);

            ctx.SaveChanges();

            this.bus.Publish(new UserCreated { UserId  = user.Id, Name = user.Name, Email = user.Email, MobilePhone = user.MobilePhone, CompanyName = company.Name });
        }
    }

}