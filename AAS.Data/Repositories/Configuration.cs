namespace AAS.Data.Migrations
{
    using AAS.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<AAS.Data.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "AAS.Data.ApplicationDbContext";
        }

        protected override void Seed(AAS.Data.ApplicationDbContext context)
        {
            if (context.Stocks.Any())
            {
                return;
            }

            var rand = new Random();

            //password 123123

            var testUser = new ApplicationUser()
            {
                Email = "test@test.bg",
                UserName = "test@test.bg",
                PasswordHash = "AEyfEgPZuktSCzOcsvfkpK0F/y9j4VxEatUYcnVIwqeBnZZ53L1rzuKzZWhuug5Kkw==",
                SecurityStamp = "7cd5c4c9-61ba-41e0-9a6a-f5148bdd5715"
            };

            context.Users.Add(testUser);
            context.SaveChanges();

            var testOwner = new Owner()
            {
                UserId = context.Users.FirstOrDefault().Id
            };

            context.Owners.Add(testOwner);
            context.SaveChanges();

            var testCompany = new Company()
            {
                Name = "testCompany",
                OwnerId = context.Owners.FirstOrDefault().Id,
                Bulstrad = "BG123321",
                Adress = "Sofia Bulgaria"
            };

            for (int i = 1; i <= 20; i++)
            {
                var testStock = new Stock()
                {
                    Name = "testStock" + i.ToString(),
                    Quantity = i * rand.Next(100),
                    Price = i * rand.Next(1, 250),
                    
                };

                testCompany.Stocks.Add(testStock);
            }


            context.Companies.Add(testCompany);
            context.SaveChanges();
            //context.Stocks.Add(testStock);


        }
    }
}
