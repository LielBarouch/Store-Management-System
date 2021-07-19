using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FInalP
{
    public class Customer
    {
        private long id;
        private string firstName;
        private string lastName;
        private string phone;
        private string address;
        private string email;

        //Constructor with no params
        public Customer() { }

        //Constructor with params
        public Customer(string firstName, string lastName, string phone, string address, string email)
        {
            this.id = -1;
            this.firstName = firstName;
            this.lastName = lastName;
            this.phone = phone;
            this.address = address;
            this.email = email;
        }
        //Getters and setters
        public long Id { get => id; set => id = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
        public string Email { get => email; set => email = value; }

        


    }
}
