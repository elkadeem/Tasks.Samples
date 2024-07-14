using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore.Basics
{
    [Table("Departments", Schema = "HumanResources")]
    public class Department
    {
        private string _name;

        private int _id;

        public Address Address { get; set; }


        [Column("Name", TypeName = "nvarchar")]
        [MaxLength(50)]        
        public string Name { get => _name; set => _name = value; }
        public int Id { get => _id; set => _id = value; }

        public List<Section> Sections { get; set; }

        public void Update()
        {

        }
    }

    public class  Section
    {
        public string Name { get; set; }
    }

    [NotMapped]
    public class Address
    {
        public string Line1 { get; set; }

        public string Line2 { get; set; }
    }
}
