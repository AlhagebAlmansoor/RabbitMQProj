using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace active_facilty.domain.complains.models
{
    public class Complain
    {
        public Complain()
        {
                
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public string TicketNumber { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Complainer Complainer { get; set; }
    }
}