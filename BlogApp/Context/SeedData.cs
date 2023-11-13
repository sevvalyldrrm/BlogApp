using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Context
{
	public class SeedData
	{
		public static void TestVerileriniDoldur(IApplicationBuilder app)
		{
			var context = app.ApplicationServices.CreateScope().ServiceProvider.GetService<DataContext>();

			if (context != null)
			{
				if (context.Database.GetPendingMigrations().Any())
				{
					context.Database.Migrate();
				}

				if (!context.Tags.Any())
				{
					context.Tags.AddRange(
						new Tag { Text = "Web Programlama" },
						new Tag { Text = "Backend" },
						new Tag { Text = "Frontend" },
						new Tag { Text = "Fullstack" },
						new Tag { Text = "PHP" }
					);
					context.SaveChanges();
				}

				if (!context.Users.Any())
				{
					context.Users.AddRange(
						new User { UserName = "sevvalyldrm" },
						new User { UserName = "ahmetyilmaz" }
					);
					context.SaveChanges();
				}

				if (!context.Post.Any())
				{
					context.Post.AddRange(
						new Post
						{
							Title = "Asp.net Core",
							Content = "Asp.net Core dersleri",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-10),
							Tags = context.Tags.Take(3).ToList(),
							Image = "1.jpg",
							UserId = 1
						},
						new Post
						{
							Title = "PHP",
							Content = "PHP Core dersleri",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-20),
							Tags = context.Tags.Take(2).ToList(),
							Image = "2.jpg",
							UserId = 1
						},
						new Post
						{
							Title = "Django",
							Content = "Django dersleri",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-5),
							Tags = context.Tags.Take(4).ToList(),
							Image = "3.jpg",
							UserId = 2
						}
					);
					context.SaveChanges();
				}
			}
		}
	}
}
