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

        public DbSet<Student> Students { get; set; }

        public DbSet<Course> Courses { get; set; }

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

                builder.HasMany<Employee>()
                .WithOne()
                //.HasForeignKey("DepartmentId");
                .HasForeignKey(c => c.DepartmentId);
                
            });

            modelBuilder.Entity<Employee>(builder =>
            {
                builder.OwnsMany(c => c.ShipingAddresses, builder =>
                {
                    builder.Property(c => c.City).HasMaxLength(50);
                    builder.Property(c => c.State).HasMaxLength(50);
                    builder.Property(c => c.Country).HasMaxLength(50);
                });

                //builder.HasOne<Department>()
                //.WithMany()
                ////.HasForeignKey("DepartmentId");
                //.HasForeignKey(c => c.DepartmentId);

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

            modelBuilder.Entity<Student>(builder => {

               builder.HasOne(c => c.Image)
                .WithOne()
                .HasForeignKey<StudentImage>(c => c.Id);

                builder.OwnsOne(c => c.Address, a =>
                {
                    a.Property(c => c.City).HasColumnName("City").HasMaxLength(50);
                    a.Property(c => c.State).HasMaxLength(50);
                    a.Property(c => c.Country).HasMaxLength(50);
                });
            
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
