using BlogApp.Entity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlogApp.Models
{
	public class PostCreateViewModel
	{
		public int PostId { get; set; }

		[Required]
		[DisplayName("Başlık")]
		public string? Title { get; set; }

		[Required]
		[DisplayName("Açıklama")]
		public string? Description { get; set; }
		
		[Required]
		[DisplayName("İçerik")]
		public string? Content { get; set; }
		
		[Required]
		[DisplayName("Url")]
		public string? Url { get; set; }

		public bool IsActive { get; set; }

		public List<Tag> Tags { get; set; } = new();
	}
}
