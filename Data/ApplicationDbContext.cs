using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SampleCrudStoreAspNetCore8WithIndentity.Models;

namespace SampleCrudStoreAspNetCore8WithIndentity.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<SampleCrudStoreAspNetCore8WithIndentity.Models.Customer> Customer { get; set; } = default!;
        public DbSet<SampleCrudStoreAspNetCore8WithIndentity.Models.Product> Product { get; set; } = default!;
        public DbSet<SampleCrudStoreAspNetCore8WithIndentity.Models.OrderApp> OrderApp { get; set; } = default!;
    }
}
