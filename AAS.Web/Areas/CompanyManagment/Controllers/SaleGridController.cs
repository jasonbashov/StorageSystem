namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AAS.Data;
    using AAS.Web.Areas.CompanyManagment.Models.Sale;

    public class SaleGridController : KendoGridManageController
    {
        public SaleGridController(IAASData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            int companyId = 0;
            try
            {
                int.TryParse(TempData["cmpId"].ToString(), out companyId);
                this.TempData["cmpId"] = companyId;

                return this.Data.Stocks.All().Where(s => s.CompanyId == companyId).AsQueryable().Project().To<SaleViewModel>();

            }
            catch (Exception)
            {

                this.TempData["Error"] = "No sales";
            }

            return null;
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Stocks.GetById(id) as T;
        }
    }
}