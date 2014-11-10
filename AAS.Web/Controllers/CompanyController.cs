﻿namespace AAS.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    using AAS.Models;
    using AAS.Web.Models;

    public class CompanyController : BaseController
    {
        // GET: Companies
        public ActionResult Index()
        {
            return View();
        }

        //[OutputCache(Duration = 15 * 60)]
        public ActionResult ViewRegisteredCompanies()
        {
            var companies = this.Data.Companies.All().ToList();

            return View(companies);
        }

        [HttpGet]
        public ActionResult CreateNewCompany()
        {
            //TODO return createNewCompany Input Model

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

            if (company.ImgUrl == null)
            {
                company.ImgUrl = "http://www.securitypros.co.za/images/frontend/main/file_1379335265.png";
            }

            var newCompany = new Company() { Name = company.Name, Adress = company.Adress, Bulstrad = company.Bulstrad, ImgUrl = company.ImgUrl };
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
            var searchedCompany = this.Data.Companies.All().FirstOrDefault(c => c.Id == id);

            if (searchedCompany == null)
            {
                return View("Error");
            }

            //TODO: create new company view model

            return View(searchedCompany);
        }

        [HttpGet]
        public ActionResult ViewMyCompanies()
        {
            if (!this.Data.Owners.All().Any(o => o.UserId == this.CurrentUser.Id))
            {
                return RedirectToAction("CreateNewCompany");
            }

            var owner = this.Data.Owners.All().FirstOrDefault(o => o.UserId == this.CurrentUser.Id);
            var myCompanies = this.Data.Companies.All().Where(c => c.OwnerId == owner.Id).ToList();

            return View("ViewRegisteredCompanies" ,myCompanies);
        }
    }
}