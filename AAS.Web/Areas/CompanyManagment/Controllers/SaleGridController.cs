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
            var companyId = int.Parse(TempData["cmpId"].ToString());
            this.TempData["cmpId"] = companyId;

            return this.Data.Stocks.All().Where(s => s.CompanyId == companyId).AsQueryable().Project().To<SaleViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Stocks.GetById(id) as T;
        }
    }
}