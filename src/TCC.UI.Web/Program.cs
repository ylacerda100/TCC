using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using TCC.Domain.Models;
using TCC.Infra.Data.Context;
using TCC.UI.Web.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDatabaseConfiguration(builder.Configuration);

builder.Services.AddDefaultIdentity<Usuario>(options => {
    options.SignIn.RequireConfirmedAccount = true;
    options.Password = new PasswordOptions
    {
        RequireDigit = false,
        RequiredLength = 6,
        RequireLowercase = false,
        RequireUppercase = false,
        RequireNonAlphanumeric = false
    };
}).AddEntityFrameworkStores<AppDbContext>();

builder.Services.ConfigureApplicationCookie(options => options.LoginPath = "/Identity/Login/Entrar");
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapperConfiguration();
builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddHttpContextAccessor();

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
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "node_modules")),
    RequestPath = new PathString("/npm")
}) ;

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
