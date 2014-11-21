namespace AAS.Web.Controllers
{
    using AAS.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class HomeController : BaseController
    {
        [OutputCache(Duration = 5 * 60)]
        public ActionResult Index()
        {
            var homeStats = new HomeStatsView();

            var companiesCount = this.Data.Companies.All();

            if (companiesCount != null)
            {
                homeStats.RegCompanies = companiesCount.Count();
            }
            else
            {
                homeStats.RegCompanies = 0;
            }

            var allClients = this.Data.Clients.All();

            if (companiesCount != null)
            {
                homeStats.RegClients = allClients.Count();
            }
            else
            {
                homeStats.RegClients = 0;
            }

            var allSales = this.Data.Sales.All();

            if (companiesCount != null)
            {
                homeStats.NumberOfSales = allSales.Count();
            }
            else
            {
                homeStats.NumberOfSales = 0;
            }

            return View(homeStats);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Our mission";

            return View();
        }

        [Authorize]
        public ActionResult Manage()
        {
            var userId = this.CurrentUser.Id;

            var userAsOwner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == userId);

            if (userAsOwner == null)
            {
                TempData["Error"] = "You are not an owner";
                return RedirectToAction("Index");
            }

            var userCompaniesAsList = this.Data.Companies.All().Where(c => c.OwnerId == userAsOwner.Id).ToList();
            
            List<SelectListItem> li = new List<SelectListItem>();

            foreach (var company in userCompaniesAsList)
            {
                li.Add(new SelectListItem { Text = company.Name, Value = company.Id.ToString() });
            }

            ViewData["country"] = li;

            return View(userCompaniesAsList);
        }

        public ActionResult Companies()
        {
            return View();
        }

        public ActionResult Error()
        {
            return View();
        }
    }
}