using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management.Business
{
    [Serializable]
    public class Customer : Person
    {
        public Customer() { }
        public Customer(string id, string firstName, string lastName, DateTime birthDate, string email, string phone) : base(firstName, lastName, birthDate)
        {
            Id = id;
            Email = email;
            Phone = phone;
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public override string ToString()
        {
            return FirstName + " " + LastName + " - " + Id;
        }
    }
}
