using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace active_facilty.domain.complains.models
{
    public class ComplainContext : DbContext
    {
        public ComplainContext() : base("active_faciltyapicomplains")
        {

        }

        public DbSet<Complain> Complains { get; set; }

        public DbSet<Complainer> Complainers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}