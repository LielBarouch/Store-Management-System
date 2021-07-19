using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalP
{
    public class Item
    {
        private long id;
        private string name;
        private double price;
        private int age;
        private int unitsInStock;
        private int qtySold;
        private long supplierId;

        //Constructor with no params
        public Item()
        {

        }
        //Constructor with params
        public Item(string name, double price, int unitsInStock, int qtySold, long supplierId)
        {
            this.id = -1;
            this.name = name;
            this.price = price;
            this.unitsInStock = unitsInStock;
            this.qtySold = qtySold;
            this.supplierId = supplierId;
        }
        //Getters and setters
        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Price { get => price; set => price = value; }
        public int UnitsInStock { get => unitsInStock; set => unitsInStock = value; }
        public int QtySold { get => qtySold; set => qtySold = value; }
        public long SupplierId { get => supplierId; set => supplierId = value; }

    }


}
