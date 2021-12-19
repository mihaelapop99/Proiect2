using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Proiect2.Models
{
    public class PhonesStore
    {
        public int StoreID { get; set; }
        public int PhoneID { get; set; }
        public Store Store { get; set; }
        public Phone Phone { get; set; }

    }
}
