using active_facilty.domain.complains.handlers;
using EasyNetQ;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace active_facilty.api.complains.Controllers
{
   
    public class ComplainsController : ApiController
    {
        
        protected IMediator mediator;

        //protected IComplainCommandHandler handler;

        public ComplainsController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public IHttpActionResult Post(CreateComplain cmd)
        {
            this.mediator.Publish(cmd);
            return Ok();
        }
    }
}