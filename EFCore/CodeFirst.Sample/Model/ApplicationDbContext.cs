using CodeFirst.Sample.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CodeFirst.Sample.Model
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {

        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<Department> Departments { get; set; }

        public DbSet<Employee> Employees { get; set; }

        public DbSet<EmployeeChild> EmployeeChildren { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=CodeFirstSampleDb;Integrated Security=True;Trust Server Certificate=True");
            optionsBuilder.LogTo(System.Console.WriteLine
                , Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("HR");
            modelBuilder.Entity<Department>(builder =>
            {
                //builder.ToTable("Departments", "HR");
                builder.Property(c => c.Name).HasMaxLength(50);
                builder.Property(c => c.Location).HasMaxLength(100);

                builder.Property<DateTime>("UpdatedDate");
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.Property(c => c.Name).HasMaxLength(100);
                builder.Property(c => c.CreatetionDate).HasDefaultValueSql("GETDATE()");

                builder.Property(c => c.UpdateDate)
                //.HasDefaultValueSql("GETDATE()")
                .HasValueGenerator<DateTimeValueGenerator>()
                .ValueGeneratedOnAddOrUpdate()
                .Metadata.SetAfterSaveBehavior(Microsoft.EntityFrameworkCore.Metadata.PropertySaveBehavior.Save);
            });

            modelBuilder.Entity<EmployeeChild>(builder =>
            {
                builder.HasKey(c => new { c.Serial, c.EmployeeId });
            });


        }
    }

    public class DateTimeValueGenerator : ValueGenerator<DateTime>
    {
        public override bool GeneratesTemporaryValues => false;

        public override DateTime Next(EntityEntry entry)
        {
            return DateTime.UtcNow; // Or use DateTime.Now, depending on your requirements
        } 
    }
}
