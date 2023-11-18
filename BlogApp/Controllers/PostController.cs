using BlogApp.Context;
using BlogApp.Entity;
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

        public async Task<IActionResult> Index(string tag)
        {

            var posts = _context.Set<Post>().AsQueryable();

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
            return View(await _context.Post.Include(x => x.Tags).Include(x => x.Comments).ThenInclude(x => x.User).FirstOrDefaultAsync(p => p.Url == url));
        }

        [HttpPost]
        public JsonResult AddComment(int PostId, string UserName, string Text)
        {
            var entity = new Comment
            {
                Text = Text,
                PublishedOn = DateTime.Now,
                PostId = PostId,
                User = new User { UserName = UserName, Image="avatar.jpg" },
            };
            _context.Add(entity);
            _context.SaveChanges();

            //return Redirect("/posts/details/" + Url);

            return Json( new{
                UserName,
                Text,
                entity.PublishedOn,
                entity.User.Image
            });
        }
    }
}
