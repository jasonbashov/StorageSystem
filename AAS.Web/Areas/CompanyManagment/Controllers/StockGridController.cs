namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using AAS.Data;
    using AAS.Web.Areas.CompanyManagment.Controllers;

    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using AAS.Models;
    using System.Collections;
    
    using Model = AAS.Models.Stock;
    using ViewModel = AAS.Web.Areas.CompanyManagment.Models.StockViewModel;
    using AAS.Web.Areas.CompanyManagment.Models;
    using System.Collections.Generic;


    public partial class StockGridController : KendoGridManageController
    {
        public StockGridController(IAASData data)
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

            return this.Data.Stocks.All().Where(s => s.CompanyId == companyId).AsQueryable().Project().To<StockViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Stocks.GetById(id) as T;
        }
        
        [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {

            model.CreatedOn = model.CreatedOn.ToLocalTime();

            var currCompanyId = this.TempData["cmpId"];
            model.CompanyId = int.Parse(currCompanyId.ToString());
            this.TempData["cmpId"] = currCompanyId;

            //var createdOnNewCulture = model.CreatedOn.ToShortDateString();
            //var asd = this.Request.Params[3];
            //var companyId = ;// int.Parse(this.Url.RequestContext.RouteData.Values["id"].ToString());
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            base.Update<Model, ViewModel>(model, model.Id);
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                this.Data.Stocks.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }
    }
}