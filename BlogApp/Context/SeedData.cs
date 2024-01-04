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
                        new Tag { Text = "Web Programlama", Url = "web-programlama", Color = TagColors.warning},
						new Tag { Text = "Backend", Url = "backend", Color = TagColors.secondary },
						new Tag { Text = "Frontend", Url = "frontend", Color = TagColors.success },
						new Tag { Text = "Fullstack", Url = "fullstack", Color = TagColors.primary },
						new Tag { Text = "PHP", Url = "php" , Color = TagColors.danger }
					);
					context.SaveChanges();
				}

				if (!context.Users.Any())
				{
					context.Users.AddRange(
						new User { UserName = "sevvalyldrm", Name= "Şevval Yıldırım", Email= "info@sevvalyldrm.com", Password="123" ,Image ="p1.jpg" },
						new User { UserName = "ahmetyilmaz", Name = "Ahmet Yılmaz", Email = "info@ahmetylmz.com", Password = "123", Image ="p2.jpg" }
					);
					context.SaveChanges();
				}

				if (!context.Post.Any())
				{
					context.Post.AddRange(
						new Post
						{
							Title = "Asp.net Core",
							Description = "Asp.net Core dersleri",
							Content = "Asp.net Core dersleri",
							Url = "aspnet-core",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-10),
							Tags = context.Tags.Take(3).ToList(),
							Image = "1.jpg",
							UserId = 1,
							Comments = new List<Comment> { 
								new Comment { Text = "iyi bir kurs" , PublishedOn =  DateTime.Now.AddDays(-20), UserId = 1}, 
								new Comment { Text = "çok faydasını gördüğüm bir kurs" , PublishedOn =  DateTime.Now.AddDays(-10), UserId = 2},
							}
						},
						new Post
						{
							Title = "PHP",
							Description = "PHP Core dersleri",
							Content = "PHP Core dersleri",
							Url = "php",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-20),
							Tags = context.Tags.Take(1).ToList(),
							Image = "2.jpg",
							UserId = 1
						},
						new Post
						{
							Title = "Django",
							Description = "Django dersleri",
							Content = "Django dersleri",
							Url = "django",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-30),
							Tags = context.Tags.Take(0).ToList(),
							Image = "3.jpg",
							UserId = 2
						},
						new Post
						{
							Title = "Angular",
							Description = "Angular dersleri",
							Content = "Angular dersleri",
							Url = "angular",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-40),
							Tags = context.Tags.Take(2).ToList(),
							Image = "1.jpg",
							UserId = 2
						},
						new Post
						{
							Title = "React Js",
							Description = "React dersleri",
							Content = "React dersleri",
							Url = "react-dersleri",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-50),
							Tags = context.Tags.Take(3).ToList(),
							Image = "2.jpg",
							UserId = 2
						},
						new Post
						{
							Title = "Web Tasarım",
							Description = "Web Tasarım dersleri",
							Content = "Web Tasarım dersleri",
							Url = "web-tasarim",
							IsActive = true,
							PublishedOn = DateTime.Now.AddDays(-60),
							Tags = context.Tags.Take(1).ToList(),
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
