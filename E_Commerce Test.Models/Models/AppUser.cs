using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_Test.Models.Models
{
    public class AppUser : IdentityUser
    {
        [Required]
        public string Full_Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

    }
}
