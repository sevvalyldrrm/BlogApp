using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlogApp.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        [DisplayName("E-posta")]
        public string? Email { get; set; }

        [Required]
        [StringLength(10,ErrorMessage = "{0} alanı en az {2} karakter olmalıdır", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string? Password { get; set; }
    }
}
