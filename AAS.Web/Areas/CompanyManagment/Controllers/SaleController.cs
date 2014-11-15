namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AAS.Web.Controllers;
    using AAS.Web.Areas.CompanyManagment.Models.Sale;
    using AutoMapper;
    using AAS.Models;
    using System.Collections.Generic;
    using AAS.Web.Areas.CompanyManagment.Models.Client;

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
                            .To<SaleViewModel>().FirstOrDefault(s => s.Id == id);

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
                TempData["Error"] = "No such sale";

                return RedirectToAction("Index", "Home", new { Area = "" });
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
        public ActionResult CreateNewSale(NewSaleInputModel newSale)
        {
            //if (!ModelState.IsValid)
            //{
            //    return View(newSale);
            //}
            var asd = newSale;

            System.Console.WriteLine();

            //TempData["Success"] = "Company created successfuly";

            return RedirectToAction("Index", "Home", new { Area = String.Empty });
        }

        public ActionResult Search(string query)
        {
            var clientViewModel = this.Data.Clients.All()
                .AsQueryable()
                .Where(c => c.Bulstrad.ToLower().Contains(query.ToLower()))
                .Project().To<ClientViewModel>()
                .FirstOrDefault();

            var result = new NewSaleInputModel()
            {
                ClientBulstrad = clientViewModel.Bulstrad,
                ClientName = clientViewModel.Name
            };

            return this.PartialView("_ClientsResult", result);
        }

        public ActionResult ChooseFromDropdown(int id)
        {
            var clients = this.Data.Companies.All().FirstOrDefault(c => c.Id == id).Clients.ToList();

            List<SelectListItem> li = new List<SelectListItem>();

            foreach (var client in clients)
            {
                li.Add(new SelectListItem { Text = client.Name, Value = client.Id.ToString() });
            }

            ViewData["clientDdl"] = li;

            return this.PartialView("_ClientDdl");
        }

        [HttpGet]
        public ActionResult SearchByBulstrad()
        {
            return PartialView("_ClientSearchForm");
        }
    }
}