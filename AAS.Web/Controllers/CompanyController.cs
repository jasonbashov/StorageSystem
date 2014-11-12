namespace AAS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    using AAS.Models;
    using AAS.Web.Models;

    using AutoMapper.QueryableExtensions;
    using AAS.Web.Models.Company;

    public class CompanyController : AuthorizeUserController
    {
        // GET: Companies
        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 15 * 60)]
        [HttpGet]
        [AllowAnonymous]
        public ActionResult ViewRegisteredCompanies(int id)
        {
            if (id < 0)
            {
                return RedirectToAction("ViewRegisteredCompanies", new { id = 0 });
            }
            var pageSize = 6;

            var companies = this.Data.Companies.All()
                                     .OrderBy(c => c.Name)
                                     .Skip(id * pageSize)
                                     .Take(pageSize)
                                     .AsQueryable().Project().To<CompanyDetailsViewModel>().ToList();

            return View(companies);
        }

        [HttpGet]
        public ActionResult CreateNewCompany()
        {
            //TODO return createNewCompany Input Model
            //var currentUserId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(this.User.Identity);

            //var currentUserAsOwner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == currentUserId);
            
            //if (currentUserAsOwner == null)
            //{
            //    return View();
            //}

            //var newCompany = new CompanyInputModel() { AccountablePerson = currentUserAsOwner.FullName };

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewCompany(CompanyInputModel company)
        {
            if (!ModelState.IsValid)
            {
                return View(company);
            }

            //give default picture
            if (company.ImgUrl == null)
            {
                company.ImgUrl = "http://www.securitypros.co.za/images/frontend/main/file_1379335265.png";
            }

            var newCompany = new Company()
            {
                Name = company.Name,
                Adress = company.Adress,
                Bulstrad = company.Bulstrad,
                ImgUrl = company.ImgUrl,
                AccountablePerson = company.AccountablePerson
            };
            var currentUserId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(this.User.Identity);

            var currentUserAsOwner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == currentUserId);

            //if there is no such owner - make the current user owner
            if (currentUserAsOwner == null)
            {
                this.Data.Owners.Add(new Owner() { UserId = currentUserId });
                this.Data.SaveChanges();
                currentUserAsOwner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == currentUserId);
            }

            newCompany.OwnerId = currentUserAsOwner.Id;

            this.Data.Companies.Add(newCompany);
            this.Data.SaveChanges();
            

            TempData["Success"] = "Company created successfuly";

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult ViewCompanyDetails(int id)
        {
            var searchedCompany = this.Data.Companies.All().AsQueryable().Project().To<CompanyDetailsViewModel>().FirstOrDefault(c => c.Id == id);

            if (searchedCompany == null)
            {
                return View("Error");
            }

            ViewBag.UserId = this.CurrentUser.Id;

            return View(searchedCompany);
        }

        [HttpGet]
        public ActionResult ViewMyCompanies(int id)
        {
            if (!this.Data.Owners.All().Any(o => o.UserId == this.CurrentUser.Id))
            {
                return RedirectToAction("CreateNewCompany");
            }

            if (id < 0)
            {
                return RedirectToAction("ViewMyCompanies", new { id = 0 });
            }

            var pageSize = 6;

            var owner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == this.CurrentUser.Id);
            var myCompanies = this.Data.Companies.All()
                                       .Where(c => c.OwnerId == owner.Id)
                                       .OrderBy(c => c.Name)
                                       .Skip(id * pageSize)
                                       .Take(pageSize)
                                       .AsQueryable().Project().To<CompanyDetailsViewModel>().ToList();

            return View(myCompanies);
        }

        [HttpGet]
        public ActionResult ManageMyCompany(int id)
        {
            return RedirectToRoute("~/CompanyManagment/Company/ManageMyCompany/" + id.ToString());
        }
    }
}