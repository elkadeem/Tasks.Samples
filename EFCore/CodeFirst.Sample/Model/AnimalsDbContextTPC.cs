using CodeFirst.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample.Model
{
    public class AnimalsDbContextTPC : DbContext
    {
        public AnimalsDbContextTPC()
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Pet> Pets { get; set; }

        //public DbSet<Dog>  Dogs { get; set; }

        public DbSet<Human> Humans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DbTPC;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(System.Console.WriteLine
                , Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<Pet>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<Human>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<Cat>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<Dog>()
                .UseTpcMappingStrategy();

            modelBuilder.Entity<FarmAnimal>()
                .UseTpcMappingStrategy();
        }
    }
}
