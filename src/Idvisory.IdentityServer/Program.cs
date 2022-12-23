using Duende.IdentityServer.AspNetIdentity;
using Idvisory.IdentityServer.Infrastructure;
using Idvisory.IdentityServer.Infrastructure.Models;
using Idvisory.IdentityServer.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<IdentityServerDbContext>(option =>
            option.UseSqlServer(connectionString));

builder.Services.AddInsfrastructureIdentityServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseIdentityServer();

app.UseAuthentication();
app.UseAuthorization();

using var scope = app.Services.CreateScope();
var identityDb = scope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
await identityDb.Database.MigrateAsync();

// database seeding
var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
await SeedingIdentityServer.Seeding(userManager, roleManager);

app.MapRazorPages();

app.Run();
