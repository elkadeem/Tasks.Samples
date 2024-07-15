using CodeFirst.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample.Model
{
    public class AnimalsDbContextWithoutInhertiance : DbContext
    {
        // Current Model Can't be used with Inheritance
        // Becuase of the following reasons:
        // 1. The base class is abstract
        // 2. The derived classes use the base class as a property
        public AnimalsDbContextWithoutInhertiance()
        {
        }

        public DbSet<FarmAnimal> FarmAnimals { get; set; }

        public DbSet<Cat> Cats { get; set; }

        public DbSet<Dog> Dogs { get; set; }

        public DbSet<Human> Humans { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DbWithoutInhertiance;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(System.Console.WriteLine
                , Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FarmAnimal>()
                .HasBaseType((Type)null);

            modelBuilder.Entity<Dog>()
                .HasBaseType((Type)null);

            modelBuilder.Entity<Cat>()
                .HasBaseType((Type)null);

            modelBuilder.Entity<Human>()
                .HasBaseType((Type)null);

            modelBuilder.Ignore<Pet>();

            modelBuilder.Ignore<Animal>();

            modelBuilder.Entity<Human>(builder =>
            {
                builder.Ignore(c => c.Pets);
                builder.Ignore(c => c.FavoriteAnimal);
            });
        }
    }
}
