using CodeFirst.Sample.Entities;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample.Model
{
    public class AnimalsDbContextTPH : DbContext
    {
        public AnimalsDbContextTPH()
        {
        }

        public AnimalsDbContextTPH(DbContextOptions<AnimalsDbContextTPC> options)
            : base(options)
        {

        }

        public DbSet<Animal> Animals { get; set; }

        public DbSet<Pet> Pets { get; set; }

        //public DbSet<Dog>  Dogs { get; set; }

        public DbSet<Human> Humans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=DbTBH;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(System.Console.WriteLine
                , Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>()
                .HasDiscriminator<string>("Discriminator")
                .HasValue<Dog>(nameof(Dog))
                .HasValue<Cat>(nameof(Cat))
                .HasValue<FarmAnimal>(nameof(FarmAnimal))
                .HasValue<Human>(nameof(Human));
        }
    }
}
