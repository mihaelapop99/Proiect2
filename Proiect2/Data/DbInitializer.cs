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
                 new Phone{Title="Iphone 13",Producer="Apple",Price=Decimal.Parse("400")},
                 new Phone{Title="Iphone 12",Producer="Apple",Price=Decimal.Parse("350")},
                 new Phone{Title="Galaxy S10",Producer="Samsung",Price=Decimal.Parse("330")},
                 new Phone{Title="Iphone 12Pro",Producer="Apple",Price=Decimal.Parse("340")},
                 new Phone{Title="Iphone 13Pro",Producer="Apple",Price=Decimal.Parse("530")},
                 new Phone{Title="Iphone 13ProMax",Producer="Apple",Price=Decimal.Parse("550")},
            };
            foreach (Phone s in phones)
            {
                context.Phones.Add(s);
            }
            context.SaveChanges();
            
            
            
            var customers = new Customer[]
            {

                 new Customer{CustomerID=1050,Name="Popescu Marcela",BirthDate=DateTime.Parse("1979-09-01")},
                 new Customer{CustomerID=1045,Name="Mihailescu Cornel",BirthDate=DateTime.Parse("1969-07-08")},

 };
            foreach (Customer c in customers)
            {
                context.Customers.Add(c);
            }
            context.SaveChanges();

            
                        var worker = new Worker[]
                       {

                             new Worker{WorkerID=10,Name="Pop Ana",HireDate=DateTime.Parse("2020-09-01")},
                             new Worker{WorkerID=12,Name="Popa Alina",HireDate=DateTime.Parse("2019-07-08")},

            };
                        foreach (Worker e in worker)
                        {
                            context.Workers.Add(e);
                        }
                        context.SaveChanges();



                        var orders = new Order[]
                        {
                                 new Order{PhoneID=1,CustomerID=1050,WorkerID=10, OrderDate=DateTime.Parse("02-25-2020")},
                                 new Order{PhoneID=3,CustomerID=1045,WorkerID=12, OrderDate=DateTime.Parse("12-2-2020")},
                                 new Order{PhoneID=1,CustomerID=1045,WorkerID=12, OrderDate=DateTime.Parse("08-15-2020")},
                                 new Order{PhoneID=2,CustomerID=1050,WorkerID=10, OrderDate=DateTime.Parse("04-25-2020")},
                        };
                        foreach (Order e in orders)
                        {
                            context.Orders.Add(e);
                        }
                        context.SaveChanges();

            var stores = new Store[]
 {

         new Store{StoreName="Altex",Adress="Str. Aviatorilor, nr. 40,Bucuresti"},
         new Store{StoreName="Emag",Adress="Str. Plopilor, nr. 35,Ploiesti"},
         new Store{StoreName="Media Galaxy",Adress="Str. Cascadelor, nr.22, Cluj-Napoca"},
         };
                    foreach (Store p in stores)
            {
                context.Stores.Add(p);
            }
            context.SaveChanges();
            var phonesstores = new PhonesStore[]
            {
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Iphone 13" ).ID,
         StoreID = stores.Single(i => i.StoreName =="Altex").ID
         },
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Iphone 12" ).ID,
         StoreID = stores.Single(i => i.StoreName =="Altex").ID
         },
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Galaxy S10" ).ID,
         StoreID = stores.Single(i => i.StoreName =="Emag").ID
         },
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Iphone 12Pro" ).ID,
        StoreID = stores.Single(i => i.StoreName == "Emag 45").ID
         },
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Iphone 13Pro" ).ID,
        StoreID = stores.Single(i => i.StoreName == "Emag").ID
         },
         new PhonesStore {
         PhoneID = phones.Single(c => c.Title == "Iphone 13ProMax" ).ID,
         StoreID = stores.Single(i => i.StoreName == "Media Galaxy").ID
         },
            };
            foreach (PhonesStore pb in phonesstores)
            {
                context.PhonesStores.Add(pb);
            }
            context.SaveChanges();


        }
    }
}
