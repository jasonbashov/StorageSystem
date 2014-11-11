using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AAS.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

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

            var userCompanies = this.Data.Companies.All().Where(c => c.OwnerId == userAsOwner.Id).ToList();

            return View(userCompanies);
        }

        public ActionResult Companies()
        {
            return View();
        }
    }
}