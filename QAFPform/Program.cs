using Microsoft.EntityFrameworkCore;
using QAFPform.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<QAFPformDbContext>(options =>
    options.UseFirebird(
        builder.Configuration.GetConnectionString("FirebirdConnection")
    ));

builder.Services.AddHealthChecks()
    .AddCheck<FirebirdHealthCheck>("firebird");


// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Formularz}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapHealthChecks("/health");

app.Run();
