using PayPortal.Infrastructure.Identity.Seeds;
//using PayPortal.Core.Application;
using PayPortal.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using StockApp.Infrastructure.Identity;
using PayPortal.Infrastructure.Identity.Entities;
//using PayPortal.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();
//builder.Services.AddApplicationLayer(builder.Configuration);
builder.Services.AddIdentityInfrastructure(builder.Configuration);
builder.Services.AddPersistenceInfrastucture(builder.Configuration);
builder.Services.AddSession();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
//builder.Services.AddTransient<ValidateUserSession, ValidateUserSession>();

var app = builder.Build();

<<<<<<< HEAD
=======
#region Inserta dessde los seed el role y el user Manager
>>>>>>> origin/YanB
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    try
    {
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultClientUser.SeedAsync(userManager, roleManager);
        await DefaultAdminUser.SeedAsync(userManager, roleManager);

    }
    catch (Exception ex)
    {

    }
}
<<<<<<< HEAD
=======
#endregion
>>>>>>> origin/YanB

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
