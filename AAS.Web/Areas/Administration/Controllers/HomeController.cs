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
            var users = this.Data.Users.All().ToList();
            

            return View(users);
        }

        public ActionResult ViewUsers()
        {
            var users = this.Data.Users.All().AsQueryable().Project().To<UsersViewModel>().ToList();

            return View();
        }
    }
}