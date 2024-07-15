using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirst.Sample.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public double Amount { get; set; }
    }

    public class InvoiceItem
    {
        public string Serial { get; set; }

        public double UnitPrice { get; set; }

        public string Quantity { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }
    }
}
