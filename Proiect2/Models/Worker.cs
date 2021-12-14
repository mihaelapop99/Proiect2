using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
namespace Proiect2.Models
{
    public class Worker
    { //Atributul DatabaseGenerated permite introducerea cheii primare pentru Customer in loc sa fie generata
        //de baza de date.
        
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int WorkerID { get; set; }
        public string Name { get; set; }
        public string Adress { get; set; }
        public DateTime HireDate { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
