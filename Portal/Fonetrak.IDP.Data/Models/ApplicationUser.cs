using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Fonetrak.IDP.Data.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public bool Active { get; set; }

        //[Required]
        //[MaxLength(150)]
        //public string FirstName { get; set; }

        //[Required]
        //[MaxLength(150)]
        //public string LastName { get; set; }

        //[MaxLength(200)]
        //public string TimeZone { get; set; }
    }
}
