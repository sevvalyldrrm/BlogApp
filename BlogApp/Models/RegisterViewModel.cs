using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace BlogApp.Models
{
	public class RegisterViewModel
	{
		[Required]
		[DisplayName("Kullanıcı Adı")]
		public string? UserName { get; set; }

		[Required]
		[DisplayName("Ad Soyad")]
		public string? Name { get; set; }


		[Required]
		[EmailAddress]
		[DisplayName("E-posta")]
		public string? Email { get; set; }

		[Required]
		[StringLength(10, ErrorMessage = "{0} alanı en az {2} karakter olmalıdır", MinimumLength = 3)]
		[DataType(DataType.Password)]
		[DisplayName("Şifre")]
		public string? Password { get; set; }

		[Required]
		[Compare(nameof(Password) , ErrorMessage="Parola eşleşmiyor.")]
		[DataType(DataType.Password)]
		[DisplayName("Şifre Tekrar")]
		public string? ConfirmPassword { get; set; }


	}
}
