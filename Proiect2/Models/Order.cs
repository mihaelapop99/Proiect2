using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect2.Models
{
    public class Order 
    {
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int WorkerID { get; set; }
        public int PhoneID { get; set; }
        public DateTime OrderDate { get; set; }
        public Customer Customer { get; set; }
        public Phone Phone { get; set; }
    }
}
