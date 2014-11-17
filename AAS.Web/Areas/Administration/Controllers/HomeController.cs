namespace AAS.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AAS.Web.Controllers;
    using AAS.Web.Areas.Administration.Models;

    public class HomeController : AdminController
    {
        // GET: Administration/Home
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ViewClients()
        {
            return View();
        }

        public ActionResult ViewUsers()
        {
            return View();
        }

        public ActionResult ViewCompanies()
        {
            return View();
        }
    }
}