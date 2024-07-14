using DatabaseFirst.Sample.Model;

namespace DatabaseFirst.Sample
{
    internal class Program
    {
        static void Main(string[] args)
        {
           AdventureWorksDbContext dbContext = new AdventureWorksDbContext();

            var departments = dbContext.Departments.ToList();
            foreach (var department in departments)
            {
                System.Console.WriteLine(department.Name);
            }

        }
    }
}
