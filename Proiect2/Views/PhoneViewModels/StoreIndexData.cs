using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect2.Models;

//namespace Proiect2.Views.PhoneViewModels
namespace Proiect2.Models.PhoneViewModels
{
    public class StoreIndexData
    {
        public IEnumerable<Store> Stores { get; set; }
        public IEnumerable<Phone> Phones { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
