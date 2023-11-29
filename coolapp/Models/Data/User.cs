using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace coolapp.Models.Data
{
    public class User : IdentityUser
    {
        public DateTime DateReg { get; set; }
    }
}
