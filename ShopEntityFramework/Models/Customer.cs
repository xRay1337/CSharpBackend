using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopEntityFramework.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [Required]
        [MaxLength(11)]
        public string PhoneNumber { get; set; }

        [Required]
        [MaxLength(255)]
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
    }
}
