using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Sample.Entities
{
    public class Student
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public List<Course> Courses { get; private set; } = new List<Course>();

        public StudentImage Image { get; set; } = new StudentImage();

        public Address Address { get; set; } = new Address();
    }

    public class StudentImage
    {
        public int Id { get; set; }

        public byte[] Image { get; set; }
    }

    public class Address
    {
        public string City { get; set; }

        public string State { get; set; }

        public string Country { get; set; }

    }
    public class Course
    {
        public int Id { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }

        public List<Student> Students { get; private set; } = new List<Student>();
    }
}
