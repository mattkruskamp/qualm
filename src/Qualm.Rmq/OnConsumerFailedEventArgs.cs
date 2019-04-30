using Qualm.Commands;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qualm.Rmq
{
    public class OnConsumerFailedEventArgs : EventArgs
    {
        public ulong DeliveryTag { get; set; }
        public IModel Channel { get; set; }
        public ICommand Command { get; set; }
        public Exception Error { get; set; }
    }
}
