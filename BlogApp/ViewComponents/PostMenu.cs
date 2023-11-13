using BlogApp.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
	public class PostMenu : ViewComponent
	{
		private DataContext _context;

		public PostMenu(DataContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(
				_context
				.Post
				.OrderByDescending(p=> p.PublishedOn)
				.Take(5)
				.ToList()
			);
		}

	}
}
