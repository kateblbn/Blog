using Blog.web.Data;
using Blog.web.Repositories;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddDbContext<AuthDbContext>(options => { options.UseSqlServer(builder.Configuration.GetConnectionString("BlogAuthConnectingString")); });
            builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContext>();

            builder.Services.Configure<IdentityOptions>(options =>
            {
                //Default settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequiredLength = 6;
            });

            builder.Services.AddScoped<Repositories.ITagRepository, TagRepository>();
            builder.Services.AddScoped<Repositories.IBlogPostRepository, BlogPostRepository>();
            builder.Services.AddScoped<Repositories.IImageRepository, CloudinaryImageRepository>();
            builder.Services.AddScoped<Repositories.IBlogPostLikeRepository, BlogPostLikeRepository>();

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
