using BlogApp.Context;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		private readonly DataContext _context;

		public PostController(DataContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View(
				new PostViewModel
				{
					Posts = _context.Post.ToList(),
					//Tags = _context.Tags.ToList()
				}
			);
		}
	}
}
