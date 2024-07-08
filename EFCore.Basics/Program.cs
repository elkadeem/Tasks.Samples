namespace EFCore.Basics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
        }
    }

    public class Department
    {
        public string Id { get; set; }

        public string Name { get; private set; }
        
    }

    public class Employee
    {
        public string Id { get; set; }

        public string Name { get; private set; }
        
        public Department Department { get; private set; }

        public Employee? Manager { get; private set; }
    }

}
