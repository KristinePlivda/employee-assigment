using System;
using System.ComponentModel.DataAnnotations;

namespace HrDataRegistration.DataAccess.Models
{
    public class Employee
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string SocialSecurityNumber { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime CreationDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}