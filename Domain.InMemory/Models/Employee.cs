using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Domain.InMemory.Models
{
    public enum Region : byte
    {
        [Description("Asia")]
        Asia = 1,
        [Description("Australia")]
        Australia = 2,
        [Description("Africa")]
        Africa = 3,
        [Description("America")]
        America = 4,
        [Description("Europe")]
        Europe = 5
    }

    public class Rank
    {
        public int Level { get; set; }
        public string Name { get; set; }
    }


    public class Employee
    {
        public Guid Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public DateTime JoinDate { get; set; }

        public Region EmployedRegion { get; set; }

        public Rank Rank { get; set; }

        public string AvatarIcon { get; set; } // as Base64String
    }
}
