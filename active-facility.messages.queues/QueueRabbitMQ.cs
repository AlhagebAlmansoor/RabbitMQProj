using EasyNetQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace active_facility.messages.queues
{
    public class QueueRabbitMQ : IMessageBus
    {
        protected IBus bus;

        public QueueRabbitMQ(IBus bus)
        {
            this.bus = bus;
        }

        public void Publish<T>(T m) where T : Messages
        {
            bus.Publish(m);
        }

        public void Subscribe(Messages m)
        {
            //
        }
    }
}
