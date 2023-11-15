using BlogApp.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.ViewComponents
{
	public class TagsMenu : ViewComponent
	{
		private DataContext _context;

		public TagsMenu(DataContext context)
		{
			_context = context;
		}

		public async Task<IViewComponentResult> InvokeAsync()
		{
			return View(await _context.Tags.ToListAsync()); //Select * From 
		}
	}
}
