using BlogApp.Context;
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
			return View(_context.Post.ToList());
		}
	}
}
