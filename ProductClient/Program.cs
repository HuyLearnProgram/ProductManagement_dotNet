using Microsoft.EntityFrameworkCore;
using ProductManagementModule.Repositories;
using ProductManagementModule.Context;
using ProductManagementModule.Services;

var builder = WebApplication.CreateBuilder(args);


// Đăng ký DbContext với chuỗi kết nối từ appsettings.json

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
//    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))));

//string connectionString = "Server=localhost;Port=3303;Database=Product;User=root;Password=123456;SslMode=Required";
//string connectionString = "Server=localhost;Port=3303;Database=Product;User=root;Password=123456;SslMode=Required";
string connectionString = "Server=mysql-grocery-store-student-66e7.f.aivencloud.com;Port=28329;Database=product;User=avnadmin;Password=AVNS_yLsGi4zDxqP7zpfy6Eb;SslMode=Required";
// Configure DbContext with MySQL connection string
builder.Services.AddDbContext<ProductDbContext>(options =>
    options.UseMySql(
        connectionString,
        ServerVersion.AutoDetect(connectionString))
);

// Đăng ký các dịch vụ
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>(); // Đảm bảo dòng này có mặt và sử dụng namespace đúng



builder.Services.AddControllersWithViews();  // Đăng ký MVC controllers

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapGet("/", () => Results.Redirect("/products/view/1"));

app.MapControllerRoute(
    name: "addProduct",
    pattern: "product/add",
    defaults: new {controller = "Product", action = "AddProductView"}
    );

app.MapControllerRoute(
    name: "productView",
    pattern: "products/view/{page}",
    defaults: new { controller = "Product", action = "ProductView", page = 1 });

app.MapControllerRoute(
    name: "productDetailView",
    pattern: "product/{id}",
    defaults: new { controller = "Product", action = "ProductDetailView" });

app.Run();
