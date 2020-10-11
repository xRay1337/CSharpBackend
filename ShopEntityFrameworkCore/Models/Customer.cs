using System.Collections.Generic;

namespace ShopEntityFrameworkCore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

        public Customer(string lastName, string firstName, string middleName, string phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleName = middleName;
            PhoneNumber = phoneNumber;
            Email = email;
        }

        public override string ToString()
        {
            return $"Id: {Id} {LastName} {FirstName} {MiddleName ?? string.Empty}";
        }
    }
}
