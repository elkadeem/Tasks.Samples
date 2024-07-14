using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Sample.Entities
{
    public class Employee
    {
        public Employee(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; private set; }

        [MaxLength(100)]
        public string Name { get; private set; }

        public DateTime CreatetionDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public void Update(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"'{nameof(name)}' cannot be null or whitespace.", nameof(name));
            }

            Name = name;

        }
    }
}
