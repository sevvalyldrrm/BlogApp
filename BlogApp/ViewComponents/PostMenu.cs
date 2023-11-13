using BlogApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
	public class PostMenu : ViewComponent
	{
		private DataContext _context;

		public PostMenu(DataContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await
				_context
				.Post
				.OrderByDescending(p=> p.PublishedOn)
				.Take(5)
				.ToListAsync()
			);
		}

	}
}
