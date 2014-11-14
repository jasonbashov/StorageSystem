namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AAS.Web.Controllers;
    using AAS.Web.Areas.CompanyManagment.Models.Sale;
    using AutoMapper;

    public class SaleController : AuthorizeUserController
    {
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

        public ActionResult SaleDetails(int id)
        {
            var sale = this.Data.Sales.All().AsQueryable().Project().To<SaleViewModel>().FirstOrDefault(s => s.Id == id);

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
    }
}