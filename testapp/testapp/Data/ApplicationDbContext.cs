using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using testapp.Models;

namespace testapp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Inventory> Inventory { get; set; }

        public DbSet<Item> Items { get; set; }

        public DbSet<ItemType>  ItemTypes { get; set; }

        public DbSet<ItemxType> ItemxTypes { get; set; }
    }
}
