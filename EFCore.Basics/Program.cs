using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
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

            

            var productCategory = new ProductCategory
            {
                Name = $"Category 14",
                ModifiedDate = DateTime.Now,
                Rowguid = Guid.NewGuid()
            };

            applicationDbContext.ProductCategories.Add(productCategory);

            for (int i = 0; i < 10; i++)
            {
                var productSubcategory = new ProductSubcategory
                {
                    Name = $"Category 14 {i}",
                    ModifiedDate = DateTime.Now,
                    Rowguid = Guid.NewGuid(),
                };

                productCategory.ProductSubcategories.Add(productSubcategory);
            }

            applicationDbContext.SaveChanges();

            Console.WriteLine("Enter Product Category Id to delete:");
            foreach (var item in applicationDbContext.ProductCategories.ToList())
            {

                productCategory.ProductSubcategories.Remove();
                applicationDbContext.Remove(item);
                Console.WriteLine($"Id: {item.ProductCategoryID}, Name: {item.Name}");
            }

            productCategory.ProductSubcategories.Clear();
            applicationDbContext.ProductCategories.Remove(productCategory);

            applicationDbContext.SaveChanges();

            //AddRelatedProductSubCategory(applicationDbContext);

            //UpdateProductCategoryAndAddOthers(applicationDbContext);



            //AddUpdateDeleteProudctCategory(applicationDbContext);

            //QueriesSamples(applicationDbContext);

        }

        private static void AddRelatedProductSubCategory(ApplicationDbContext applicationDbContext)
        {
            var productCategory = new ProductCategory
            {
                Name = $"Category 11",
                ModifiedDate = DateTime.Now,
                Rowguid = Guid.NewGuid()
            };

            applicationDbContext.ProductCategories.Add(productCategory);

            for (int i = 0; i < 10; i++)
            {
                var productSubcategory = new ProductSubcategory
                {
                    Name = $"Subcategory {i}",
                    ModifiedDate = DateTime.Now,
                    Rowguid = Guid.NewGuid(),
                };

                productCategory.ProductSubcategories.Add(productSubcategory);
            }

            applicationDbContext.SaveChanges();
        }

        private static void UpdateProductCategoryAndAddOthers(ApplicationDbContext applicationDbContext)
        {
            var productCategory = applicationDbContext.ProductCategories.Find(9);
            Console.WriteLine($"Category: {productCategory.Name}");
            productCategory.Name = Console.ReadLine();

            for (int x = 1; x < 10; x++)
            {
                productCategory = new ProductCategory
                {
                    Name = $"Category {x}",
                    ModifiedDate = DateTime.Now,
                    Rowguid = Guid.NewGuid()
                };

                applicationDbContext.ProductCategories.Add(productCategory);
            }

            applicationDbContext.SaveChanges();
        }

        private static void AddUpdateDeleteProudctCategory(ApplicationDbContext applicationDbContext)
        {
            ProductCategory productCategory = new ProductCategory()
            {
                Name = Console.ReadLine(),
                ModifiedDate = DateTime.Now,
                Rowguid = Guid.NewGuid()
            };

            applicationDbContext.ProductCategories.Add(productCategory);

            var entry = applicationDbContext.Entry(productCategory);
            Console.WriteLine($"State: {entry.State}");

            applicationDbContext.SaveChanges();

            Console.WriteLine($"Product Category Id: {productCategory.ProductCategoryID}");
            Console.WriteLine($"State: {entry.State}");

            Console.WriteLine("Update Product Category Name:");
            productCategory.Name = Console.ReadLine();
            entry = applicationDbContext.Entry(productCategory);
            Console.WriteLine($"State: {entry.State}");

            applicationDbContext.SaveChanges();

            Console.WriteLine($"State: {entry.State}");

            Console.WriteLine("Delete Product Category:");
            applicationDbContext.ProductCategories.Remove(productCategory);
            entry = applicationDbContext.Entry(productCategory);
            Console.WriteLine($"State: {entry.State}");
            applicationDbContext.SaveChanges();
            Console.WriteLine($"State: {entry.State}");
        }

        private static void QueriesSamples(ApplicationDbContext applicationDbContext)
        {
            ProductCategoriesSamples(applicationDbContext);

            var items = applicationDbContext.Set<ProductSubcategory>().ToList();
            EagerLoadingSample(applicationDbContext);

            ExplicitLoadingSample(applicationDbContext);

            LazyLoadingSample(applicationDbContext);

            SelectProductsWithPagination(applicationDbContext);

            ProductWithFilter(applicationDbContext);

            ProductWithFilterWithInclude(applicationDbContext);

            FindProduct(applicationDbContext);

            ProductGrouping(applicationDbContext);

            GroupingWithMax(applicationDbContext);
        }

        private static void GroupingWithMax(ApplicationDbContext applicationDbContext)
        {
            applicationDbContext.Products
                            .GroupBy(c => c.ProductSubcategory.ProductCategory.Name)
                            .Select(c => new { Category = c.Key, Max = c.Max(c => c.ListPrice) });

            var categoryWithHighestPrice = applicationDbContext.Products
                 .GroupBy(c => c.ProductSubcategory.ProductCategory.Name)
                 .Select(c => new { Category = c.Key, MaxProduct = c.OrderByDescending(c => c.ListPrice).FirstOrDefault() });

            foreach (var item in categoryWithHighestPrice)
            {
                Console.WriteLine($"Category: {item.Category}, Product: {item.MaxProduct?.Name}, Price: {item.MaxProduct?.ListPrice}");
            }
        }

        private static void FindProduct(ApplicationDbContext applicationDbContext)
        {
            var product = applicationDbContext.Products.Find(1);

            Console.WriteLine(JsonSerializer.Serialize(product, new JsonSerializerOptions { WriteIndented = true }));
        }

        private static void ProductGrouping(ApplicationDbContext applicationDbContext)
        {
            var productCategoryProducts = applicationDbContext
                            .Products
                            .GroupBy(c => c.ProductSubcategory.ProductCategory.Name)
                            .Select(c => new { Category = c.Key, Count = c.Count() });

            foreach (var item in productCategoryProducts)
            {
                Console.WriteLine($"Category: {item.Category}, Count: {item.Count}");
            }
        }

        private static void ProductWithFilterWithInclude(ApplicationDbContext applicationDbContext)
        {
            var bikesProductsWithInclude = applicationDbContext.Products
                            .Include(c => c.ProductSubcategory.ProductCategory)
                            //.Include(c => c.ProductSubcategory)
                            //.ThenInclude(c => c.ProductCategory)
                            .Where(c => c.ProductSubcategory.ProductCategory.Name == "Bikes");

            foreach (var item in bikesProductsWithInclude)
            {
                Console.WriteLine($"Product: {item.Name}");
            }
        }

        private static void ProductWithFilter(ApplicationDbContext applicationDbContext)
        {
            var bikesProducts = applicationDbContext.Products
                            .Where(c => c.ProductSubcategory.ProductCategory.Name == "Bikes");

            foreach (var item in bikesProducts)
            {
                Console.WriteLine($"Product: {item.Name}");
            }
        }

        private static void SelectProductsWithPagination(ApplicationDbContext applicationDbContext)
        {
            int pageSize = 20;
            int index = 0;
            // Skip 0 (index * pageSize) & Take 20 (pageSize)
            // Skip 20 & Take 20
            // Skip 40 & Take 20
            // Last page Total Count / pageSize
            IQueryable<Product> productList = applicationDbContext.Products;

            productList = productList.Where(c => c.Name.StartsWith("A"));
            productList = productList.Where(c => c.ListPrice > 0);

            int totalCount = productList.Count();
            int pageCount = (int)Math.Ceiling((double)totalCount / pageSize);

            for (int i = 0; i < pageCount; i++)
            {
                var items = productList
                    .OrderBy(c => c.Name)
                    .Skip(i * pageSize).Take(pageSize).ToList();
                Console.WriteLine($"Page: {i + 1}");
                foreach (var item in items)
                {
                    Console.WriteLine($"Product: {item.Name}");
                }
            }
        }

        private static void EagerLoadingSample(ApplicationDbContext applicationDbContext)
        {
            //Eage Loading include ProductCategory
            var items = applicationDbContext.ProductSubcategories
                .Include(s => s.ProductCategory)
                .ToList();
            foreach (var item in items)
            {
                Console.WriteLine($"Subcategory: {item.Name}, Category: {item.ProductCategory.Name}");
            }
        }

        private static void ExplicitLoadingSample(ApplicationDbContext applicationDbContext)
        {
            // Explicitly include ProductCategory
            var items2 = applicationDbContext.ProductSubcategories.ToList();

            foreach (var item in items2)
            {
                applicationDbContext
                    .Entry(item)
                    .Reference(s => s.ProductCategory)
                    .Load();
                Console.WriteLine($"Subcategory: {item.Name}, Category: {item.ProductCategory.Name}");
            }
        }

        private static void LazyLoadingSample(ApplicationDbContext applicationDbContext)
        {
            // Lazy Loading
            foreach (var item in applicationDbContext.ProductSubcategories.ToList())
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
