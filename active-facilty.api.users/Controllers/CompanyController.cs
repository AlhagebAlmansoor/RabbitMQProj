using active_facility.domain.users.handlers;
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
    public class CompanyController : ApiController
    {
        protected IMediator mediator;

        public CompanyController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public IHttpActionResult Post(CreateCompany cmd)
        {
            this.mediator.Publish(cmd);

            return Ok();
        }

        public Task<List<AllCompanyView>> Get()
        {
            return this.mediator.Send<List<AllCompanyView>>(new GetAllCompany());
        }
    }
}
