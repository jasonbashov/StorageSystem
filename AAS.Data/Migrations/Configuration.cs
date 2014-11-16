namespace AAS.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;
    
    using Microsoft.AspNet.Identity.EntityFramework;

    using AAS.Common;
    using AAS.Models;

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
            if (context.Users.Any())
            {
                return;
            }

            //password 123123
            this.SeedRoles(context);
            this.AddUsers(context);
            this.AddOwnerAndClient(context);
            this.AddCompany(context);

            var rand = new Random();
            var testCompany = context.Companies.FirstOrDefault();

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

            var soldStock = new SoldStock()
            {
                CompanyId = testCompany.Id,
                Name = "testSoldStock",
                QuantitySold = 5,
                SingleUnitPrice = 10,
                SumPrice = (5 * 10),
            };

            context.SoldStocks.Add(soldStock);
            context.SaveChanges();

            var testSale = new Sale()
            {
                ClientId = context.Clients.FirstOrDefault().Id,
                CompanyId = testCompany.Id,
                DateOfSale = DateTime.Now,
                TotalCost = soldStock.SumPrice
            };

            testSale.SoldStocks.Add(context.SoldStocks.FirstOrDefault());

            context.Sales.Add(testSale);
            context.SaveChanges();
            //context.Stocks.Add(testStock);
        }

        private void SeedRoles(ApplicationDbContext context)
        {
            context.Roles.AddOrUpdate(x => x.Name, new IdentityRole(GlobalConstants.AdminRole));
            context.SaveChanges();
        }

        private void AddCompany(ApplicationDbContext context)
        {
            var testCompany = new Company()
            {
                Name = "testCompany",
                OwnerId = context.Owners.FirstOrDefault().Id,
                Bulstat = "BG123456789",
                Adress = "Sofia Bulgaria"
            };

            context.Companies.Add(testCompany);
            context.SaveChanges();
        }

        private void AddOwnerAndClient(ApplicationDbContext context)
        {
            var testOwner = new Owner()
            {
                UserId = context.Users.FirstOrDefault(u => u.Email == "test@test.bg").Id
            };

            var testClientUser = context.Users.FirstOrDefault(u => u.Email == "testClient@test.bg");

            var testClient = new Client()
            {
                UserId = testClientUser.Id,
                Bulstat = "BG321954786",
                Name = "Gosho Goshov",
                Adress = "new adress"
            };

            context.Owners.Add(testOwner);
            context.Clients.Add(testClient);
            context.SaveChanges();
        }

        private void AddUsers(ApplicationDbContext context)
        {
            var testUser = new ApplicationUser()
            {
                Email = "test@test.bg",
                UserName = "test@test.bg",
                PasswordHash = "AEyfEgPZuktSCzOcsvfkpK0F/y9j4VxEatUYcnVIwqeBnZZ53L1rzuKzZWhuug5Kkw==",
                SecurityStamp = "7cd5c4c9-61ba-41e0-9a6a-f5148bdd5715"
            };

            var testUserToBeClient = new ApplicationUser()
            {
                Email = "testClient@test.bg",
                UserName = "testClient@test.bg",
                PasswordHash = "AEyfEgPZuktSCzOcsvfkpK0F/y9j4VxEatUYcnVIwqeBnZZ53L1rzuKzZWhuug5Kkw==",
                SecurityStamp = "7cd5c4c9-61ba-41e0-9a6a-f5148bdd5715"
            };

            context.Users.Add(testUser);
            context.Users.Add(testUserToBeClient);
            context.SaveChanges();
        }
    }
}
