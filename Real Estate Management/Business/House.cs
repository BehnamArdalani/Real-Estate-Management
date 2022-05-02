using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Real_Estate_Management.Business
{
    [Serializable]
    public class House:Property
    {
        public House() { }
        public House(string id, string street, string number, string city, string province, string country, string postalCode,double livingArea, int bedRooms, double price, string ownerId)
        {
            Id = id;
            Street = street;
            Number = number;
            City = city;
            Province = province;
            Country = country;
            PostalCode = postalCode;
            LivingArea = livingArea;
            BedRooms = bedRooms;
            Price = price;
            OwnerId = ownerId;
        }

        public string Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public double LivingArea { get; set; }
        public int BedRooms { get; set; }
        public double Price { get; set; }
        public string OwnerId { get; set; }

        public override string ToString()
        {
            return Number + " " + Street + " " + City + ", " + PostalCode + " - " + Id;
        }
    }
}
