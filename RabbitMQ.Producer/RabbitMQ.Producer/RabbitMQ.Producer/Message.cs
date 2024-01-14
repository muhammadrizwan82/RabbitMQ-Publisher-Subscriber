using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RabbitMQ.Producer
{
    public class Message
    {
        public string? name { get; set; }
        public string? message { get; set; }
        public string? messageId { get; set; }
    }
}
