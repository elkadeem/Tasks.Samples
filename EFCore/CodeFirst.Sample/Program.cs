using CodeFirst.Sample.Entities;
using CodeFirst.Sample.Model;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //SampleQueries();

            AnimalsDbContextTPH animalsDbContextTPH = new AnimalsDbContextTPH();
            var items = await animalsDbContextTPH.Animals.AsNoTracking().ToListAsync();
            

            Console.WriteLine("TPH: ================================");
            await AddAnimalsAndHuman(new AnimalsDbContextTPH());
            await SelectAnimalsAndHuman(new AnimalsDbContextTPH());

            Console.WriteLine("TPT: ================================");
            await AddAnimalsAndHuman(new AnimalsDbContextTPT());
            await SelectAnimalsAndHuman(new AnimalsDbContextTPT());

            Console.WriteLine("TPC: ================================");
            await AddAnimalsAndHuman(new AnimalsDbContextTPC());
            await SelectAnimalsAndHuman(new AnimalsDbContextTPC());

            

        }

        public static async Task SelectAnimalsAndHuman(DbContext dbContext)
        {
            Console.WriteLine("All Animals:");
            foreach (var animal in dbContext.Set<Animal>())
            {
                Console.WriteLine($"Animal: {animal.Name}, Type: {animal.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Pets:");
            foreach (var pet in dbContext.Set<Pet>())
            {
                Console.WriteLine($"Pet: {pet.Name}, Type: {pet.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Cats:");
            foreach (var pet in dbContext.Set<Pet>().OfType<Cat>())
            {
                Console.WriteLine($"Pet: {pet.Name}, Type: {pet.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Humans:");
            foreach (var human in dbContext.Set<Human>())
            {
                Console.WriteLine($"Human: {human.Name}, Type: {human.GetType().Name}");
            }
        }

        public static async Task AddAnimalsAndHuman(DbContext dbContext)
        {
            dbContext.Set<Animal>().Add(new Cat("Cat", "A")
            {
                Food = Food.CatFood,
                Vet = "Vet1"
            });

            dbContext.Set<Animal>().Add(new Dog("Dog", "Toy")
            {
                Vet = "Vet2"
            });

            dbContext.Set<Animal>().Add(new FarmAnimal("FarmAnimal", "Farm")
            {
                Value = 1000,
            });

            dbContext.Set<Pet>().Add(new Cat("Cat2", "A")
            {
                Food = Food.CatFood,
                Vet = "Vet1"
            });

            dbContext.Set<Pet>().Add(new Dog("Dog2", "Toy")
            {
                Vet = "Vet2"
            });

            dbContext.Set<Human>().Add(new Human("Human")
            {
                Food = Food.HumansFood,
            });

            int rows = await dbContext.SaveChangesAsync();
            Console.WriteLine($"Rows affected: {rows}");
        }

        private static void SampleQueries()
        {
            ApplicationDbContext dbContext = new ApplicationDbContext();
            Console.WriteLine(dbContext.Database.CanConnect());

            //Employee employee = new Employee(0, "Ahmed");
            //dbContext.Employees.Add(employee);
            //dbContext.SaveChanges();

            //Console.WriteLine($"Employee added successfully with id: {employee.Id}");

            var department = dbContext.Departments.Find(1);

            dbContext
                .Entry(department).Collection(c => c.Employees)
                .Query().Where(c => c.Name.StartsWith("A"))
                .Load();


            dbContext = new ApplicationDbContext();
            // Eager Loading
            var firstEmployee = dbContext.Employees
                .Include(c => c.Department).FirstOrDefault(c => c.Id == 12);

            firstEmployee = dbContext.Employees.Find(12);

            dbContext.Entry(firstEmployee).Reference(c => c.Department
            ).Load();

            firstEmployee.Update("Ali1254");
            firstEmployee.UpdateDate = DateTime.Now;
            dbContext.SaveChanges();

            foreach (var emp in dbContext.Employees)
            {
                Console.WriteLine($"Employee Id: {emp.Id}, Name: {emp.Name}, Created: {emp.CreatetionDate}, Updated: {emp.UpdateDate}");
            }



#if DEBUG
            //dbContext.Database.EnsureCreated();
            //dbContext.Database.Migrate();
#endif
        }
    }


    

}
