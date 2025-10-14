using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luxify.Core
{
    public class Order
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public Guid BuyerId { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
