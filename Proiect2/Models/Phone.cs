using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Proiect2.Models
{
    public class Phone
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Producer { get; set; }
        [Column(TypeName = "decimal(6, 2)")]
        public decimal Price { get; set; }
        public ICollection<Order> Orders { get; set; }

        public ICollection<PhonesStore> PhonesStore { get; set; }
    }
}
