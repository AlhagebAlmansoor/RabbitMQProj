using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.messages
{
    public class ComplainCreated : Messages
    {
        public ComplainCreated(string title)
        {
            this.Title = title;
        }

        public string Title { get; private set; }
    }
}
