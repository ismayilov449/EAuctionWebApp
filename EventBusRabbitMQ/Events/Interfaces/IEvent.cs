﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusRabbitMQ.Events.Interfaces
{
    public abstract class IEvent
    {
        public Guid RequestId { get; private init; }
        public DateTime CreatedDate { get; private init; }

        public IEvent()
        {
            RequestId = Guid.NewGuid();
            CreatedDate = DateTime.UtcNow;
        }

    }
}
