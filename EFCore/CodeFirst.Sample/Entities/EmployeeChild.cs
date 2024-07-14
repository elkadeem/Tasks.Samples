using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Sample.Entities
{
    //[PrimaryKey(nameof(Serial), nameof(EmployeeId))]
    public class EmployeeChild
    {

        public int Serial { get; set; }

        public int EmployeeId { get; set; }

        public string Name { get; set; }
    }
}
