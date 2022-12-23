using IdentityModel;
using Idvisory.IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Idvisory.IdentityServer.Infrastructure;

public class SeedingIdentityServer
{
    public static async Task Seeding(UserManager<ApplicationUser> userManager
        , RoleManager<IdentityRole> roleManager)
    {
        var adminRole = await roleManager.FindByNameAsync(SD.Admin);
        if (adminRole is not null) return;

        await roleManager.CreateAsync(new IdentityRole(SD.Admin));
        await roleManager.CreateAsync(new IdentityRole(SD.User));

        var adminUser = new ApplicationUser("admin", SD.Admin, SD.Admin,
             Models.Enums.GenderEnum.Male, DateTimeOffset.Parse("01-4-1998")
            )
        {
            Email = "admin@advisory.com",
            EmailConfirmed = true,
            PhoneNumber = "01128779841"
        };

        await userManager.CreateAsync(adminUser, "Password1!");
        await userManager.AddToRoleAsync(adminUser, SD.Admin);

        var adminClaims = new Claim[]
        {
                new Claim(JwtClaimTypes.Name,adminUser.FirstName ),
                new Claim(JwtClaimTypes.GivenName,adminUser.FirstName+" "+ adminUser.LastName),
                new Claim(JwtClaimTypes.FamilyName,adminUser.LastName),
                new Claim(JwtClaimTypes.Role,SD.Admin)
        };

        await userManager.AddClaimsAsync(adminUser, adminClaims);

        var user = new ApplicationUser("user", SD.User, SD.User,
             Models.Enums.GenderEnum.Male, DateTimeOffset.Parse("01-4-2090"))
        {
            Email = "user@advisory.com",
            EmailConfirmed = true,
            PhoneNumber = "01145742521",
        };

        await userManager.CreateAsync(user, "Password1!");
        await userManager.AddToRoleAsync(user, SD.User);

        var userClaims = new Claim[]
        {
                new Claim(JwtClaimTypes.Name, user.FirstName),
                new Claim(JwtClaimTypes.GivenName, user.FirstName +" "+ user.LastName),
                new Claim(JwtClaimTypes.FamilyName, user.LastName),
                new Claim(JwtClaimTypes.PhoneNumber, user.PhoneNumber),
                new Claim(JwtClaimTypes.Address, user.Address ?? string.Empty),
                new Claim(JwtClaimTypes.BirthDate, user.PhoneNumber),
                new Claim(JwtClaimTypes.Gender, user.PhoneNumber),
                new Claim(JwtClaimTypes.Role, SD.User),
        };

        await userManager.AddClaimsAsync(user, userClaims);
    }
}
