namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;
    
    using AAS.Models;
    using AAS.Web.Areas.CompanyManagment.Models.Client;
    using AAS.Web.Areas.CompanyManagment.Models.Sale;
    using AAS.Web.Controllers;


    public class ClientController : AuthorizeUserController
    {
        [HttpGet]
        public ActionResult CreateNewClient()
        {
            return PartialView("_CreateNewClient");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateNewClient(ClientInputModel inputClient)
        {
            if (!ModelState.IsValid || inputClient == null)
            {
                return PartialView("_CreateNewClient", inputClient);
            }

            var newClient = new Client()
            {
                Name = inputClient.Name,
                Adress = inputClient.Adress,
                Bulstat = inputClient.Bulstat
            };

            this.Data.Clients.Add(newClient);
            this.Data.SaveChanges();

            var dbClient = this.Data.Clients.All().FirstOrDefault(c => c.Bulstat == inputClient.Bulstat);

            var result = new NewSaleInputModel()
            {
                ClientBulstat = dbClient.Bulstat,
                ClientName = dbClient.Name,
                ClientId = dbClient.Id
            };

            return PartialView("_ClientsResult", result);
        }

        [HttpPost]
        public ActionResult Search(string query)
        {
            var clientViewModel = this.Data.Clients.All()
                .AsQueryable()
                .Where(c => c.Bulstat.ToLower().Contains(query.ToLower()))
                .Project().To<ClientViewModel>()
                .FirstOrDefault();

            if (clientViewModel == null)
            {
                //TempData["Error"] = "No shuch user with this bulstat";
                return this.PartialView("_ClientsResult");
            }

            var result = new NewSaleInputModel()
            {
                ClientBulstat = clientViewModel.Bulstat,
                ClientName = clientViewModel.Name
            };

            return this.PartialView("_ClientsResult", result);
        }

        [HttpGet]
        public ActionResult ChooseFromDropdown(int id)
        {
            var clients = this.Data.Companies.All().FirstOrDefault(c => c.Id == id).Clients.ToList();

            List<SelectListItem> li = new List<SelectListItem>();

            foreach (var client in clients)
            {
                li.Add(new SelectListItem { Text = client.Name, Value = client.Id.ToString() });
            }

            ViewData["clientDdl"] = li;

            return this.PartialView("_ClientDdl");
        }

        [HttpGet]
        public ActionResult SearchByBulstrad()
        {
            return PartialView("_ClientSearchForm");
        }
    }
}