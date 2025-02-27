using System.ComponentModel.DataAnnotations;

namespace IMS_Dashboard.Models.Entities
{
    public class ApplicationUser
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string Username { get; set; }

        [Required, MinLength(6)]
        public string PasswordHash { get; set; }
    }
}
