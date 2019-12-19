using CaseStudy.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace CaseStudy.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Brand> Brand { get; set; }

        public virtual DbSet<Order> Order { get; set; }

        public virtual DbSet<OrderLineItem> OrderLineItem { get; set; }

        public virtual DbSet<Branch> Branches { get; set; }


    }
}