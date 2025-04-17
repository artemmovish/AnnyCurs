using AdminApp.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace AdminApp.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Attraction> Attractions { get; set; }

        public DbSet<AttractionImage> AttractionImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<City> Cities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=E:\\Project\\Учебный процесс\\КПиЯП\\Cursach\\AnnyCurs\\AdminApp\\database.db");
    }
}
