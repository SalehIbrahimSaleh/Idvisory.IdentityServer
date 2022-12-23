using Duende.IdentityServer.Extensions;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Services;
using IdentityModel;
using Idvisory.IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Idvisory.IdentityServer.Infrastructure.Services;
public class ProfileService : IProfileService
{
    private UserManager<ApplicationUser> _userManager;

    public ProfileService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task GetProfileDataAsync(ProfileDataRequestContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        var cliams = new List<Claim>();
        cliams = cliams.Where(a => context.RequestedClaimTypes.Contains(a.Type)).ToList();

        cliams.Add(new Claim(JwtClaimTypes.Name, user.UserName));
        cliams.Add(new Claim(JwtClaimTypes.GivenName, user.FirstName));
        cliams.Add(new Claim(JwtClaimTypes.FamilyName, user.LastName));

        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            cliams.Add(new Claim(JwtClaimTypes.Role, role));
        }

        context.IssuedClaims = cliams;
    }

    public async Task IsActiveAsync(IsActiveContext context)
    {
        var sub = context.Subject.GetSubjectId();
        var user = await _userManager.FindByIdAsync(sub);

        context.IsActive = user != null;
    }
}
