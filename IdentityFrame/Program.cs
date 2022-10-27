using IdentityFrame.data;
using IdentityFrame.Models;
using IdentityFrame.Services;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<IdentityContext>();
builder.Services.AddScoped<IAccount, AccountServices>();

//register identity services
builder.Services.AddIdentity<ApplicaionUser, IdentityRole >().AddEntityFrameworkStores<IdentityContext>();

//this is part to decide which option of password you want or not 
builder.Services.Configure<IdentityOptions>(options =>
    {
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 3;
        options.Password.RequireDigit = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;

    });
builder.Services.ConfigureApplicationCookie(config =>
{
    config.LoginPath = "/Account/Signin";
    //related with remember me section to config still login by days , hours , min ypu can choice any configure time 
    config.ExpireTimeSpan = TimeSpan.FromDays(2);

});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
