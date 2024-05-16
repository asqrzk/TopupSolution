using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace TopupProject.Entities
{
	public class DataSeeder
	{
        public static void SeedData(TopupContext context)
        {
            context.Database.EnsureCreated();

            if (!context.Customers.Any())
            {
                var customer1 = context.Customers.Add(new Customer { Username = "admin", Password = "admin", IsActive = true });
                var customer2 = context.Customers.Add(new Customer { Username = "asqrzk", Password = "asqrzk", IsActive = false });

                var ben1 = context.Beneficiaries.Add(new Beneficiary { Nickname = "Ashique"});
                var ben2 = context.Beneficiaries.Add(new Beneficiary { Nickname = "Kainote"});
                var ben3 = context.Beneficiaries.Add(new Beneficiary { Nickname = "John" });
                var ben4 = context.Beneficiaries.Add(new Beneficiary { Nickname = "Doe" });
                context.SaveChanges();

                customer1.Entity.Beneficiaries.Add(ben1.Entity);
                customer1.Entity.Beneficiaries.Add(ben2.Entity);
                customer2.Entity.Beneficiaries.Add(ben3.Entity);
                customer2.Entity.Beneficiaries.Add(ben4.Entity);
                context.SaveChanges();
            }
        }
    }
}

