using Blog.web.Data;
using Blog.web.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Blog.web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<BlogDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("BlogDbConnectionString")));

            builder.Services.AddScoped<Repositories.ITagRepository, TagRepository>();
            builder.Services.AddScoped<Repositories.IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<Repositories.IImageRepository, CloudinaryImageRepository>();  

            var app = builder.Build();

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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
