using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facilty.messages.users
{
    public class UserCreated : INotification
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobilePhone { get; set; }
        public string CompanyName { get; set; }
        public Guid UserId { get; set; }
    }
}