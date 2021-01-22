using System;
using System.Collections.Generic;
using System.Text;

namespace Altkom.Orange.Models
{
    public class Order : BaseEntity
    {
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }

        // shadow property
        // private int _CustomerId 

        public ICollection<OrderDetail> Details { get; set; }
    }

    public class OrderDetail : BaseEntity
    {
        public Item Item { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
    }

    public abstract class Item : BaseEntity
    {
        public string Name { get; set; }
        public decimal UnitPrice { get; set; }
    }

    public class Product : Item
    {
        public string Color { get; set; }
        public float Weight { get; set; }
    }

    public class Service : Item
    {
        public TimeSpan Duration { get; set; }
    }
}
