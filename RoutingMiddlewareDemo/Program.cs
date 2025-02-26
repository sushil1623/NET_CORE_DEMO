using RoutingMiddlewareDemo.CustomConstraint;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRouting(option =>
{
    option.ConstraintMap.Add("IdGreaterThenTen", typeof(GreaterThenTenId));
});
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
     name: "CustomRoute",
     pattern: "AddProduct/{id:IdGreaterThenTen?}",
     defaults:new {controller= "Product",action= "AddProduct" }
     );

app.MapControllerRoute(
     name: "Default",
     pattern: "{controller=Home}/{action=Index}/{id?}"
     );
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}")
//    .WithSt
//    aticAssets();


app.Run();
