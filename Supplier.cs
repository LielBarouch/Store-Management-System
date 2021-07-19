using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalP
{
    public class Supplier
    {
        private long id;
        private string name;
        private string phone;

        //Constructor with no params
        public Supplier() { }

        //Constructor with params
        public Supplier(string name,string phone)
        {
            this.id = -1;
            this.name = name;
            this.phone = phone;
        }

        //Getters and setters
        public long Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Phone { get => phone; set => phone = value; }
    }
}
