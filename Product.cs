using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P02_SalesDatabase.Models
{
    internal class Product
    {
        
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; } 
        public ICollection<Sale> Sales { get; set; }
    }
}
