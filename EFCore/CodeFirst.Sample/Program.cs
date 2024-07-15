using CodeFirst.Sample.Entities;
using CodeFirst.Sample.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;

namespace CodeFirst.Sample
{
    internal class Program
    {
        static void Main(string[] args)
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
