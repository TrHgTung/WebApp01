using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using WebApp01.Models;
using WebApp01.Repository;
using WebApp01.Sessions;

var builder = WebApplication.CreateBuilder(args);

// Connection DB
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:ConnectedDb"]);
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);  // Hen gio end Session
    options.Cookie.IsEssential = true;
});

builder.Services.AddIdentity<AppUserModel,IdentityRole>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings.
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequiredLength = 4;
    //options.SignIn.RequireConfirmedAccount = false;
    options.User.RequireUniqueEmail = true;
});

var app = builder.Build();

app.UseStatusCodePagesWithRedirects("/Home/Error?statuscode={0}");

app.UseSession();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    //app.UseHsts();
}
app.UseStaticFiles();


app.UseRouting();
//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute( // Home Route ("/") cua trang admin
    name: "Areas",
    pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

app.MapControllerRoute( // Brand Route
    name: "Brand",
    pattern: "/Brand/{Slug?}",
    defaults: new { controller = "Brand", action = "Index" });

app.MapControllerRoute( // Category Route
    name: "Category",
    pattern: "/Category/{Slug?}",
    defaults: new {controller="Category",action="Index"});

app.MapControllerRoute( // Route default; cac route khac phai nam o tren default
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


// seeding data
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
SeedData.SeedingData(context);

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var roles = new[] { "Customer", "Admin" };

    foreach (var role in roles)
    {
        if(!await roleManager.RoleExistsAsync(role))
        {
            await roleManager.CreateAsync(new IdentityRole(role));
        }
    }
}

//using (var scope = app.Services.CreateScope())
//{
//    UserManager<IdentityUser> userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

//    string Email = "admin@mail.com";
//    string Password = "Test@123";
//    if(await userManager.FindByEmailAsync(Email) == null)
//    {
//        var user = new IdentityUser();
//        user.UserName = Email;
//        user.Email = Email;

//        await userManager.CreateAsync(user, Password);

//        await userManager.AddToRoleAsync(user, "Customer");
//    }

//}

app.Run();
