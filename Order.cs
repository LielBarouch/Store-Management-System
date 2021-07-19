using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalP
{
    public class Order
    {
        private long id;
        private double price;
        private string orderDate;
        private int count;
        private long customerId;
        private long itemId;

        //Constructor with no params
        public Order() { }

        //Constructor with params
        public Order(double price, string orderDate, int count, long customerId, long itemId)
        {
            this.id = -1;
            this.price = price;
            this.orderDate = orderDate;
            this.count = count;
            this.customerId = customerId;
            this.itemId = itemId;
        }

        //Getters and setters
        public long Id { get => id; set => id = value; }
        public double Price { get => price; set => price = value; }
        public string OrderDate { get => orderDate; set => orderDate = value; }
        public int Count { get => count; set => count = value; }
        public long CustomerId { get => customerId; set => customerId = value; }
        public long ItemId { get => itemId; set => itemId = value; }

        
    }
}
