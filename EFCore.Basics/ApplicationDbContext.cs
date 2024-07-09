using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore.Basics
{
    internal class ApplicationDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\mssqllocaldb;Initial Catalog=AdventureWorks2019;Integrated Security=True;Trust Server Certificate=True");
            
            optionsBuilder.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
            optionsBuilder.EnableSensitiveDataLogging();

            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<ProductCategory> ProductCategories { get; set; }

        public DbSet<ProductSubcategory> ProductSubcategories { get; set; }
    }

    [Table("ProductCategory", Schema = "Production")]
    public class ProductCategory
    {
        public int ProductCategoryID { get; set; }

        public string Name { get; set; }

        public Guid Rowguid { get; set; }

        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductSubcategory> ProductSubcategories { get; set; }
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
}
