using System.ComponentModel.DataAnnotations;

namespace DatingAppLatest.API.DTOs
{
    public class UserRegistrationDTO
    {
        [Required]
        public string UserName { get; set; }

    [Required]
    [StringLength(12,MinimumLength=4,ErrorMessage="Password min length is 4 and max length is 12")]

        public string Password { get; set; }

        
    }


}