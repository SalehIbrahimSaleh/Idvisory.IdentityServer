using Idvisory.IdentityServer.Infrastructure.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Idvisory.IdentityServer.Infrastructure;
public class IdentityServerDbContext : IdentityDbContext<ApplicationUser>
{
    public IdentityServerDbContext(DbContextOptions options) : base(options)
    {
    }

    protected IdentityServerDbContext()
    {
    }
}
