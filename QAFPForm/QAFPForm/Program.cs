using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace QAFPForm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            var app = builder.Build();


            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseStaticFiles();
            app.UseRouting();


            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Form}/{action=Index}/{id?}");


            app.Run();
        }
    }
}
