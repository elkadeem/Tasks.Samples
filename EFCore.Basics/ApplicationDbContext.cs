using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net.Sockets;

namespace EFCore.Basics
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AdventureWorks2019;Integrated Security=True;Trust Server Certificate=True");
            
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

            // To Enable lazy loading
            //optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<Department> Departments { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Department>()
                .ToTable("Departments", schema: "HumanResources")
                .Property(c => c.Name).HasColumnName("Name");

            
            modelBuilder.Entity<Department>(builder => { 
                builder.ToView("Departments", schema: "HumanResources");
                builder.Property(c => c.Name).HasColumnName("Name")
                .HasColumnType("nvarchar").HasMaxLength(50)
                .HasDefaultValue("Ahmed")
                ;
                
            });
            //modelBuilder.Entity<ProductCategory>()
            //    .HasMany(c => c.ProductSubcategories)
            //    .WithOne(c => c.ProductCategory)
            //    .OnDelete(DeleteBehavior.ClientCascade);
        }
    }

    [Table("ProductCategory", Schema = "Production")]
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; } = new List<ProductSubcategory>();
    }

    [Table("ProductSubcategory", Schema = "Production")]
    public class ProductSubcategory
    {
        public int ProductSubcategoryID { get; set; }

        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }

    }

    [Table("Product", Schema = "Production")]
    public class Product
    {
        public int ProductID { get; set; }

        public string Name { get; set; }

        public string ProductNumber { get; set; }

        public bool MakeFlag { get; set; }

        public bool FinishedGoodsFlag { get; set; }

        public string? Color { get; set; }

        public short SafetyStockLevel { get; set; }

        public short ReorderPoint { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public string? Size { get; set; }

        public string? SizeUnitMeasureCode { get; set; }

        public string? WeightUnitMeasureCode { get; set; }

        public decimal? Weight { get; set; }

        public int DaysToManufacture { get; set; }

        public string? ProductLine { get; set; }

        public string? Class {  get; set; }

        public string? Style { get; set; }

        
        public int? ProductSubCategoryId { get; set; }

        public int? ProductModelID { get; set; }

        public DateTime SellStartDate { get; set; }

        public DateTime? SellEndDate { get; set; }

        public DateTime? DiscontinuedDate { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public ProductSubcategory ProductSubcategory { get; set; }
    }

}
