using Microsoft.EntityFrameworkCore;
using RamenStore.Domain.Entities.Broths;
using RamenStore.Domain.Entities.Orders;
using RamenStore.Domain.Entities.Proteins;
namespace RamenStore.Infrastructure;

public class ApplicationDbContext : DbContext
{
    public DbSet<Broth> Broths { get; set; }
    public DbSet<Protein> Proteins { get; set; }
    public DbSet<Order> Orders { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
      : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Broth>().HasKey(b => b.Id);
        modelBuilder.Entity<Protein>().HasKey(p => p.Id);
        modelBuilder.Entity<Order>().HasKey(o => o.Id);

        modelBuilder.Entity<Broth>().HasData(
            new Broth("1", "https://tech.redventures.com.br/icons/salt/inactive.svg", "https://tech.redventures.com.br/icons/salt/active.svg", "Salt", "Simple like the seawater, nothing more", 10),
            new Broth("2", "https://tech.redventures.com.br/icons/shoyu/inactive.svg", "https://tech.redventures.com.br/icons/shoyu/active.svg", "Shoyu", "The good old and traditional soy sauce", 10),
            new Broth("3", "https://tech.redventures.com.br/icons/miso/inactive.svg", "https://tech.redventures.com.br/icons/miso/active.svg", "Miso", "Paste made of fermented soybeans", 12)
        );

        modelBuilder.Entity<Protein>().HasData(
            new Protein("1", "https://tech.redventures.com.br/icons/pork/inactive.svg", "https://tech.redventures.com.br/icons/pork/active.svg", "Chasu", "A sliced flavourful pork meat with a selection of season vegetables.", 10),
            new Protein("2", "https://tech.redventures.com.br/icons/yasai/inactive.svg", "https://tech.redventures.com.br/icons/yasai/active.svg", "Yasai Vegetarian", "A delicious vegetarian lamen with a selection of season vegetables.", 10),
            new Protein("3", "https://tech.redventures.com.br/icons/chicken/inactive.svg", "https://tech.redventures.com.br/icons/chicken/active.svg", "Karaague", "Three units of fried chicken, moyashi, ajitama egg and other vegetables.", 12)
        );
    }
}
