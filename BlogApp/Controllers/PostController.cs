using BlogApp.Context;
using BlogApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

		public async Task<IActionResult> Details(int? id)
		{
			return View(await _context.Post.FirstOrDefaultAsync(p => p.PostId == id));
		}
	}
}
