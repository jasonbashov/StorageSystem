namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AAS.Web.Controllers;

    public class CompanyController : AuthorizeUserController
    {
        [HttpGet]
        public ActionResult ManageMyCompany(int id)
        {
            var searchedCompany = this.Data.Companies.All().First(c => c.Id == id);

            if (searchedCompany == null)
            {
                TempData["Error"] = "Such company does not exists";
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            if (this.CurrentUser.Id != searchedCompany.Owner.UserId)
            {
                TempData["Error"] = "You are not the owner of this company";
                return RedirectToAction("Index", "Home", new { Area = "" });
            }

            return View();
        }
    }
}