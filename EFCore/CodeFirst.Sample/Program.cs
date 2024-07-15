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
            animalsDbContextTPH.Animals.Add(new Cat("Cat", "A")
            {
                Food = Food.CatFood,
                Vet = "Vet1"
            });

            animalsDbContextTPH.Animals.Add(new Dog("Dog", "Toy")
            {
                Vet = "Vet2"
            });

            animalsDbContextTPH.Animals.Add(new FarmAnimal("FarmAnimal", "Farm")
            {
                Value = 1000,
            });

            animalsDbContextTPH.Pets.Add(new Cat("Cat2", "A")
            {
                Food = Food.CatFood,
                Vet = "Vet1"
            });

            animalsDbContextTPH.Pets.Add(new Dog("Dog2", "Toy")
            {
                Vet = "Vet2"
            });

            animalsDbContextTPH.Humans.Add(new Human("Human")
            {
                Food = Food.HumansFood,                
            });

            int rows = await animalsDbContextTPH.SaveChangesAsync();
            Console.WriteLine($"Rows affected: {rows}");

            animalsDbContextTPH = new AnimalsDbContextTPH();
            Console.WriteLine("All Animals:");
            foreach (var animal in animalsDbContextTPH.Animals)
            {
                Console.WriteLine($"Animal: {animal.Name}, Type: {animal.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Pets:");
            foreach (var pet in animalsDbContextTPH.Pets)
            {
                Console.WriteLine($"Pet: {pet.Name}, Type: {pet.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Cats:");
            foreach (var pet in animalsDbContextTPH.Pets.OfType<Cat>())
            {
                Console.WriteLine($"Pet: {pet.Name}, Type: {pet.GetType().Name}");
            }

            Console.WriteLine(Environment.NewLine);
            Console.WriteLine("All Humans:");
            foreach (var human in animalsDbContextTPH.Humans)
            {
                Console.WriteLine($"Human: {human.Name}, Type: {human.GetType().Name}");
            }



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
