using Microsoft.EntityFrameworkCore;

namespace E_Bill.Models
{
    public class BillContext :DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server= DESKTOP-N92AEE4;DataBase= BillContext;Trusted_Connection=True;TrustServerCertificate=True");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Bill> Bills { set; get; }
        public DbSet<Item> Items { set; get; }
    }
}
