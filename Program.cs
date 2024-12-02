using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;


var builder = WebApplication.CreateBuilder(args);


//services
builder.Services.AddMvc();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<UrlContext>(options =>
    options.UseSqlite("Filename=urls.db"));


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

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-DNS-Prefetch-Control", "off");
    await next.Invoke();
});



app.MapControllerRoute(
    name: "shortURL",
    pattern: "www.focusmr.de/{shortCode}",
    defaults: new { controller = "Home", action = "RedirectToOriginal" }
    );



app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
