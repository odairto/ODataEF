using Microsoft.EntityFrameworkCore;
using ODataEF.Data.Entities;

namespace ODataEF.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Car> Cars  { get; set; }
        public DbSet<Manufacturer> Manufacturers  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=ODataEF;Trusted_Connection=True;");
        }
    }
}