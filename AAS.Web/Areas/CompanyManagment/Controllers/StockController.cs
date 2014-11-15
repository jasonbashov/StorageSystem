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
    using AAS.Web.Areas.CompanyManagment.Models.Sale;

    public class StockController : AuthorizeUserController
    {
        // GET: CompanyManagment/Home
        [HttpGet]
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

        [HttpGet]
        public ActionResult AddNewStocks(int id, int count)
        {
            var stocks = this.Data.Stocks.All()
                .Where(s => s.CompanyId == id && s.Quantity > 0).AsQueryable()
                .Project().To<StockViewModel>();

            List<SelectListItem> li = new List<SelectListItem>();

            ViewBag.StocksCount = count;

            foreach (var stock in stocks)
            {
                li.Add(new SelectListItem { Text = stock.Name, Value = stock.Id.ToString() });
            }

            ViewData["stockDdl"] = li;

            return PartialView("_StocksForm");
        }

        //[HttpPost]
        //public ActionResult ChooseStock(StockViewModel choosenStock)
        //{
        //    var soldStock = new SoldStockViewModel()
        //    {
        //        Name = choosenStock.Name,
        //        SingleUnitPrice = choosenStock.Price,
        //        CompanyId = choosenStock.CompanyId
        //    };

        //    var newSaleInputModel = new NewSaleInputModel();
        //    newSaleInputModel.SoldStocks = new List<SoldStockViewModel>();

        //    newSaleInputModel.SoldStocks.Add(soldStock);


        //    return PartialView("_StockResult", newSaleInputModel);
        //}
    }
}