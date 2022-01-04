﻿//using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Proiect2.Models
{
    public class Store 
    {
        public int ID { get; set; }
        [Required]
        [Display(Name = "Store Name")]
        [StringLength(50)]
        public string StoreName { get; set; }

        [StringLength(70)]
        public string Adress { get; set; }
        public ICollection<PhonesStore> PhonesStores { get; set; }
    }//un magazin poate publica mai multe telefonae, de aceea PhonesStore este definita ca o colectie
}
