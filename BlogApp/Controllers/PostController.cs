using BlogApp.Context;
using BlogApp.Entity;
using BlogApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using static System.Net.Mime.MediaTypeNames;

namespace BlogApp.Controllers
{
	public class PostController : Controller
	{
		private readonly DataContext _context;

		public PostController(DataContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index(string tag)
		{
			//var claims = User.Claims;

			var posts = _context.Set<Post>().AsQueryable().Where(i => i.IsActive);

			if (!string.IsNullOrEmpty(tag))
			{
				posts = posts.Where(p => p.Tags.Any(t => t.Url == tag));
			}

			return View(
				new PostViewModel
				{
					Posts = await posts.ToListAsync(),
					//Tags = _context.Tags.ToList()
				}
			);
		}

		public async Task<IActionResult> Details(string url)
		{
			return View(await _context.Post.Include(x => x.User).Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url));
		}

		[HttpPost]
		public JsonResult AddComment(int PostId, string Text)
		{
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
			var username = User.FindFirstValue(ClaimTypes.Name);
			var avatar = User.FindFirstValue(ClaimTypes.UserData);
			var name = User.FindFirstValue(ClaimTypes.GivenName);

			var entity = new Comment
			{
				PostId = PostId,
				Text = Text,
				PublishedOn = DateTime.Now,
				UserId = int.Parse(userId ?? "")
			};
			_context.Add(entity);
			_context.SaveChanges();

			//return Redirect("/posts/details/" + Url);

			return Json(new
			{
				name,
				username,
				Text,
				entity.PublishedOn,
				avatar
			});
		}

		[Authorize]
		public IActionResult Create()
		{
			return View();
		}

		[HttpPost]
		public IActionResult Create(PostCreateViewModel model)
		{
			if (ModelState.IsValid)
			{
				var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

				var post = new Post
				{
					Title = model.Title,
					Content = model.Content,
					Description = model.Description,
					Url = model.Url,
					PublishedOn = DateTime.Now,
					UserId = int.Parse(userId ?? ""),
					Image = "1.jpg",
					IsActive = false
				};
				_context.Add(post);
				_context.SaveChanges();

				return RedirectToAction(nameof(Index));
			}
			return View(model);
		}

		[Authorize]
		public async Task<IActionResult> List()
		{
			var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? "");
			var role = User.FindFirstValue(ClaimTypes.Role);


			var posts = _context.Set<Post>().AsQueryable();

			if (string.IsNullOrEmpty(role))
			{
				posts = posts.Where(x => x.UserId == userId);
			}
			return View(await posts.ToListAsync());
		}

		[Authorize]
		public IActionResult Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}
			var post = _context.Post.Include(i => i.Tags).FirstOrDefault(i => i.PostId == id);
			if (post == null)
			{
				return NotFound();
			}

			ViewBag.Tags = _context.Tags.ToList();

			return View(new PostCreateViewModel
			{
				PostId = post.PostId,
				Title = post.Title,
				Description = post.Description,
				Content = post.Content,
				Url = post.Url,
				IsActive = post.IsActive,
				Tags = post.Tags
			});
		}

		[Authorize]
		[HttpPost]
		public IActionResult Edit(PostCreateViewModel model, int[] tagIds)
		{
			if (ModelState.IsValid)
			{
				var entityToUpdate = _context.Post.Include(i => i.Tags).FirstOrDefault(i => i.PostId == model.PostId);

				entityToUpdate.PostId = model.PostId;
				entityToUpdate.Title = model.Title;
				entityToUpdate.Description = model.Description;
				entityToUpdate.Content = model.Content;
				entityToUpdate.Url = model.Url;
				entityToUpdate.IsActive = model.IsActive;
				entityToUpdate.Tags = _context.Tags.Where(tag => tagIds.Contains(tag.TagId)).ToList();

				if (User.FindFirstValue(ClaimTypes.Role) == "admin")
				{
					entityToUpdate.IsActive = model.IsActive;
				}

				_context.Post.Update(entityToUpdate);
				_context.SaveChanges();

				return RedirectToAction(nameof(List));

			}
			return View(model);
		}
	}
}
