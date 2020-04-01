using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Valuefirst.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public string Password { get; set; }
        [Required]
        public string Name { get; set; }
        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mobile no. is required")]
        [RegularExpression("^(?!0+$)(\\+\\d{1,3}[- ]?)?(?!0+$)\\d{10,15}$", ErrorMessage = "Please enter valid phone no.")]
        public string Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public string Status { get; set; }
        public string UserRoles { get; set; }
        
    }
}