namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    
    using AAS.Web.Controllers;
    using AAS.Web.Areas.CompanyManagment.Models;

    public class StockController : AuthorizeUserController
    {
        // GET: CompanyManagment/Home
        public ActionResult ViewMyStocks(int id)
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

            var companyStocks = this.Data.Stocks.All().Where(s => s.CompanyId == currCompany.Id)
                .AsQueryable().Project().To<StockViewModel>().ToList();// currCompany.Stocks.AsQueryable().Project().To<StockViewModel>().ToList();

            return View(companyStocks);
        }
    }
}