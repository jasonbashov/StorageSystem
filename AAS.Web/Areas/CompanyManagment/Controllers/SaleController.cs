namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    
    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using AAS.Web.Controllers;
    using AAS.Models;
    using AAS.Web.Areas.CompanyManagment.Models;
    using AAS.Web.Areas.CompanyManagment.Models.Client;
    using AAS.Web.Areas.CompanyManagment.Models.Sale;

    public class SaleController : AuthorizeUserController
    {
        [HttpGet]
        public ActionResult ViewMySales(int id)
        {
            var currCompany = this.Data.Companies.All().FirstOrDefault(c => c.Id == id);

            if (currCompany == null)
            {
                TempData["Error"] = "No such company";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (this.CurrentUser.Id != currCompany.Owner.UserId)
            {
                TempData["Error"] = "You are not the owner of this company";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var companySales = this.Data.Sales.All().Where(s => s.CompanyId == currCompany.Id)
                .AsQueryable().Project().To<SaleViewModel>().ToList();// currCompany.Stocks.AsQueryable().Project().To<StockViewModel>().ToList();

            return View(companySales);
        }

        [HttpGet]
        public ActionResult SaleDetails(int id)
        {
            var sale = this.Data
                            .Sales.All()
                            .AsQueryable()
                            .Project()
                            .To<SaleDetailsViewModel>().FirstOrDefault(s => s.Id == id);

            if (sale == null)
            {
                TempData["Error"] = "No such sale";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var company = this.Data.Companies.All().FirstOrDefault(c => c.Id == sale.CompanyId);

            if (this.CurrentUser.Id != company.Owner.UserId)
            {
                TempData["Error"] = "You are not the owner of this company";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            return View(sale);
        }

        [HttpGet]
        public ActionResult MakeNewSale(int id)
        {
            var company = this.Data.Companies.All().FirstOrDefault(c => c.Id == id);
             
            if (company == null)
            {
                TempData["Error"] = "No such company";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (!company.Stocks.Any())
            {
                TempData["Error"] = "You have no stocks in the company";

                return RedirectToAction("ManageMyCompany/" + company.Id , "Company", new { Area = "CompanyManagment" });
            }

            if (this.CurrentUser.Id != company.Owner.UserId)
            {
                TempData["Error"] = "You are not the owner of this company";

                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            var newSale = new NewSaleInputModel()
            {
                CompanyName = company.Name,
                CompanyId = company.Id,
            };

            return View(newSale);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeNewSale(int id, NewSaleInputModel newSale)
        {
            var currClient = this.Data.Clients.All().FirstOrDefault(c => (c.Bulstat == newSale.ClientBulstat) || (c.Id == newSale.ClientId));
            
            if (currClient == null)
            {
                TempData["Error"] = "No user with such id";
                return View("MakeNewSale", newSale);
            }

            newSale.ClientBulstat = currClient.Bulstat;

            if (!ModelState.IsValid)
            {
                return View("MakeNewSale", newSale);
            }

            var companyStocks = this.Data.Stocks.All().Where(s => s.CompanyId == id).ToList();

            var totalCostOfTheSale = 0.0;
            var soldStocks = newSale.SoldStocks;
            var soldStocksList = new List<SoldStock>();

            foreach (var stock in soldStocks)
            {
                var currStock = companyStocks.First(c => c.Id == stock.Id);
                if (currStock.Quantity >= stock.QuantitySold)
                {
                    currStock.Quantity -= stock.QuantitySold;
                    totalCostOfTheSale += stock.QuantitySold * currStock.Price;

                    var soldStock = new SoldStock()
                    {
                        CompanyId = id,
                        Name = currStock.Name,
                        QuantitySold = stock.QuantitySold,
                        SingleUnitPrice = currStock.Price,
                        SumPrice = ((decimal)currStock.Price * stock.QuantitySold)
                    };

                    soldStocksList.Add(soldStock);
                    this.Data.Stocks.Update(currStock);
                }
                else
                {
                    TempData["Error"] = "There are not enough quanityt of the stock " + currStock.Name;
                    return View("MakeNewSale");
                }
            }

            var currCompany = this.Data.Companies.All().FirstOrDefault(c => c.Id == id);
            //var companyClient = currCompany.Clients.Contains(currClient);

            if (!currCompany.Clients.Contains(currClient))
            {
                currCompany.Clients.Add(currClient);
                this.Data.SaveChanges(); // this may be unnessecery
            }

            this.Data.Companies.Update(currCompany);

            var sale = new Sale()
            {
                ClientId = currClient.Id,
                CompanyId = id,
                DateOfSale = newSale.DateOfSale,
                SoldStocks = soldStocksList,
                TotalCost = (decimal)totalCostOfTheSale
            };

            this.Data.Sales.Add(sale);
            this.Data.SaveChanges();

            var newSaleFromDb = this.Data.Sales.All().FirstOrDefault(s => s.DateOfSale == sale.DateOfSale);
            var newSaleId = 0;

            if (newSaleFromDb != null)
            {
                newSaleId = newSaleFromDb.Id;
            }

            TempData["Success"] = "New sale created successfuly";
            //TODO: redirect to "faktura" page

            return RedirectToAction("SaleDetails", "Sale", new { id = newSaleId });
        }
        
        

    }
}