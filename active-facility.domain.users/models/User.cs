using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace active_facilty.domain.users.models
{
    public class User
    {
        public User()
        {

        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public string MobilePhone { get; set; }

        //Company
        public Guid CompanyId { get; set; }
        //public virtual Company Company { get; set; }
    }
}