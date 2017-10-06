using active_facilty.domain.users.models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.domain.users.handlers
{
    public class GetAllCompany : IRequest<List<AllCompanyView>>
    {

    }
    public class AllCompanyView
    {
        public AllCompanyView()
        {

        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CompanyQueryHandler : IRequestHandler<GetAllCompany, List<AllCompanyView>>
    {
        protected UserContext ctx;

        public CompanyQueryHandler(UserContext ctx)
        {
            this.ctx = ctx;
        }

        public List<AllCompanyView> Handle(GetAllCompany message)
        {
            return this.ctx.Companies.Select(c => new AllCompanyView {Id=c.Id, Name = c.Name }).ToList();
        }
    }
}
