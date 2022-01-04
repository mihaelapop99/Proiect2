using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Proiect2.Models.PhoneViewModels
{
    public class OrderGroup
    {

        [DataType(DataType.Date)]
        public DateTime? OrderDate { get; set; }
        public int PhoneCount { get; set; }
        public int Price { get; set; }

        //public string WorkerId { get; set; }

    }
}