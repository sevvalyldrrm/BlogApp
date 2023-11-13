using BlogApp.Context;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.ViewComponents
{
	public class TagsMenu : ViewComponent
	{
		private DataContext _context;

		public TagsMenu(DataContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			return View(_context.Tags.ToList());
		}
	}
}
