using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.messages.queues
{
    
    public interface IMessageBus
    {
        void Publish<T>(T m) where T : Messages;
        void Subscribe(Messages m);
    }
}
