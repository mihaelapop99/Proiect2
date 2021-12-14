using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Proiect2.Models;

namespace Proiect2.Data
{
    public class DbInitializer
    {
        public static void Initialize(PhoneContext context)
        {
            context.Database.EnsureCreated();
            if (context.Phones.Any())
            {
                return; // BD a fost creata anterior
            }
            var phones = new Phone[]
            {
                 new Phone{Title="Baltagul",Producer="Mihail Sadoveanu",Price=Decimal.Parse("22")},
                 new Phone{Title="Enigma Otiliei",Producer="George Calinescu",Price=Decimal.Parse("18")},
                 new Phone{Title="Maytrei",Producer="Mircea Eliade",Price=Decimal.Parse("27")},
                 
            };
            foreach (Phone s in phones)
            {
                context.Phones.Add(s);
            }
            context.SaveChanges();
            
            
            
            var customers = new Customer[]
            {

                 new Customer{CustomerID=1050,Name="PopescuMarcela",BirthDate=DateTime.Parse("1979-09-01")},
                 new Customer{CustomerID=1045,Name="MihailescuCornel",BirthDate=DateTime.Parse("1969-07-08")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            
                        var worker = new Worker[]
                       {

                             new Worker{WorkerID=10,Name="PopescuMarcela",HireDate=DateTime.Parse("2020-09-01")},
                             new Worker{WorkerID=12,Name="MihailescuCornel",HireDate=DateTime.Parse("2019-07-08")},

            };
                        foreach (Worker e in worker)
                        {
                            context.Workers.Add(e);
                        }
                        context.SaveChanges();



                        var orders = new Order[]
                        {
                                 new Order{PhoneID=1,CustomerID=1050,WorkerID=10},
                                 new Order{PhoneID=3,CustomerID=1045,WorkerID=12},
                                 new Order{PhoneID=1,CustomerID=1045,WorkerID=12},
                                 new Order{PhoneID=2,CustomerID=1050,WorkerID=10},
                        };
                        foreach (Order e in orders)
                        {
                            context.Orders.Add(e);
                        }
                        context.SaveChanges();
          



        }
    }
}
