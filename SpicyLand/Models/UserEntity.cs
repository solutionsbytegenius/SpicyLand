using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace SpicyLand.Models
{
    [Keyless]
    public class UserEntity
    {
        public Guid UserID { get; set; }
        public required string UserName { get; set; } 
        public required string Password { get; set; }
        public DateTime DataCreazione { get; set; }
        public bool Admin {  get; set; }
        public bool SuperAdmin { get; set; }
    }
}
