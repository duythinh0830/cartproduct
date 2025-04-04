


using CartProduct.Models;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseInMemoryDatabase("ShopDb"));
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<CartService>();
builder.Services.AddSession();

var app = builder.Build();


using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    context.Products.AddRange(
        new Product { Name = "Nike Air Force 1", Price = 2990000, Description = "Đôi giày huyền thoại với thiết kế cổ điển, da cao cấp và đế giữa Air-Sole êm ái" },
        new Product { Name = " Adidas Ultraboost 22", Price = 3990000, Description = "Thích hợp cho thể thao và đi bộ đường dài." },
        new Product { Name = "Vans Old Skool", Price = 1650000, Description = "Giày sneaker phong cách trượt ván, phần thân vải và da lộn phối hợp chắc chắn" }
    );
    context.SaveChanges();
}
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
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Product}/{action=Index}/{id?}");

app.Run();
