using BlogApp.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.Context
{
	public class DataContext : DbContext
	{

		//constructor
		public DataContext(DbContextOptions<DataContext> options) : base(options) { }

		public DbSet<Post> Post => Set<Post>();	

		public DbSet<Comment> Comments => Set<Comment>();

		public DbSet<Tag> Tags => Set<Tag>();

		public DbSet<User> Users => Set<User>();
	}
}
