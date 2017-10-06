using active_facilty.domain.users.handlers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace active_facilty.api.users.Controllers
{
   
    public class UsersController : ApiController
    {

        protected IMediator mediator;
        //protected UserContext ctx;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public IHttpActionResult Post([FromBody]CreateUser cmd)
        {
            //Executre Command
            this.mediator.Publish(cmd);

            return Ok();
        }
    }
}
