using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace EFCore.Basics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext applicationDbContext = new ApplicationDbContext();
            Console.WriteLine($"Is SQL: {applicationDbContext.Database.IsSqlServer}" +
                $" and can connect: {applicationDbContext.Database.CanConnect()}");

            //ProductCategoriesSamples(applicationDbContext);

            //var items = applicationDbContext.Set<ProductSubcategory>().ToList();
            // Implicitly include ProductCategory
            //var items = applicationDbContext.ProductSubcategories
            //    .Include(s => s.ProductCategory)
            //    .ToList();
            //foreach (var item in items)
            //{                
            //    Console.WriteLine($"Subcategory: {item.Name}, Category: {item.ProductCategory.Name}");
            //}

            // Explicitly include ProductCategory
            //var items2 = applicationDbContext.ProductSubcategories.ToList(); 
            
            //foreach (var item in items2)
            //{
            //    applicationDbContext
            //        .Entry(item)
            //        .Reference(s => s.ProductCategory)
            //        .Load();
            //    Console.WriteLine($"Subcategory: {item.Name}, Category: {item.ProductCategory.Name}");
            //}

            // Lazy Loading
            foreach(var item in applicationDbContext.ProductSubcategories.ToList())
            {
                Console.WriteLine($"Subcategory: {item.Name}, Category: {item.ProductCategory.Name}");
            }

        }

        private static void ProductCategoriesSamples(ApplicationDbContext applicationDbContext)
        {
            DisplayProductCategories(applicationDbContext.ProductCategories);

            var orderdCategroies = applicationDbContext
                .ProductCategories
                .OrderBy(c => c.Name);

            Console.WriteLine("Ordered categories:");
            DisplayProductCategories(orderdCategroies);

            var productCategoryItems = applicationDbContext
                .ProductCategories
                .Where(c => c.ProductCategoryID >= 1
                && (c.Name.Contains("c")
                || c.Name.Contains("A")))
                .Select(c => new { c.Name })
                .OrderBy(c => c.Name);

            Console.WriteLine("Ordered categories with select:");
            foreach (var item in productCategoryItems)
            {
                Console.WriteLine($"Category: {item.Name}");
            }

            var productCategoryCount = applicationDbContext
                .ProductCategories
                .Count();
            Console.WriteLine(productCategoryCount);
        }

        private static void DisplayProductCategories(IEnumerable<ProductCategory> categories)
        {
            foreach (var category in categories)
            {
                Console.WriteLine($"Category: {category.Name}");
            }
        }
    }
}
