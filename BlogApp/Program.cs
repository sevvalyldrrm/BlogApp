using BlogApp.Context;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<BlogApp.Context.DataContext>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DBConStr"));
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
	options.LoginPath = "/User/Login";
});

var app = builder.Build();



SeedData.TestVerileriniDoldur(app);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

//localhost://posts/react-dersleri

app.MapControllerRoute(
	name: "post_details",
	pattern: "posts/details/{url}",
	defaults: new {controller = "Post", action="Details"}
);

app.MapControllerRoute(
	name: "post_by_tag",
	pattern: "posts/tag/{tag}",
	defaults: new {controller = "Post", action="Index"}
);

app.MapControllerRoute(
		name: "AddComment",
		pattern: "posts/AddComment",
		defaults: new { controller = "Post", action = "AddComment" }
);

app.MapControllerRoute(
		name: "user_profile",
		pattern: "profile/{username}",
		defaults: new { controller = "User", action = "Profile" }
);

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Post}/{action=Index}/{id?}");

app.Run();
