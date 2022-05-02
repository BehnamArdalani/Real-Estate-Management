using System;

namespace Real_Estate_Management.Business
{
    [Serializable]
    public class User : Person
    {
        public User() { }
        public User(string id, string firstName, string lastName, DateTime birthDate, string email, string phone, double baseSalary , double commission , bool isAdmin , string password) : base(firstName, lastName, birthDate)
        {
            Id = id;
            Email = email;
            Phone = phone;
            BaseSalary = baseSalary;
            Commission = commission;
            IsAdmin = isAdmin;
            Password = password;
            
        }
        public string Id { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public double BaseSalary { get; set; }
        public double Commission { get; set; }
        public bool IsAdmin { get; set; }
        public string Password { get; set; }
        public override string ToString()
        {
            return FirstName + " " + LastName + " - " + Id;
        }

    }
}
