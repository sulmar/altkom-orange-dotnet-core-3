using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Orange.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
    }
}
