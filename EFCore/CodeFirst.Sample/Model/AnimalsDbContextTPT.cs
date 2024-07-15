using CodeFirst.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample.Model
{
    public class AnimalsDbContextTPT : DbContext
    {
        public AnimalsDbContextTPT()
        {
        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Pet> Pets { get; set; }

        //public DbSet<Dog>  Dogs { get; set; }

        public DbSet<Human> Humans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DbTPT;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(System.Console.WriteLine
                , Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .UseTptMappingStrategy();

            modelBuilder.Entity<Pet>()
                .UseTptMappingStrategy();

            modelBuilder.Entity<Human>()
                .UseTptMappingStrategy();

            modelBuilder.Entity<Cat>()
                .UseTptMappingStrategy();

            modelBuilder.Entity<Dog>()
                .UseTptMappingStrategy();

            modelBuilder.Entity<FarmAnimal>()
                .UseTptMappingStrategy();
        }
    }
}
