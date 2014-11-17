namespace AAS.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Globalization;
    using System.Web.Mvc;
    
    using AutoMapper.QueryableExtensions;

    using Kendo.Mvc.UI;
    
    using AAS.Data;
    using AAS.Web.Areas.CompanyManagment.Controllers;

    using Model = AAS.Models.Company;
    using ViewModel = AAS.Web.Areas.Administration.Models.CompanyViewModel;

    public class CompanyGridController : KendoGridManageController
    {
        public CompanyGridController(IAASData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Companies.All().Project().To<ViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Companies.GetById(id) as T;
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
                this.Data.Companies.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

         [HttpPost]
        public ActionResult Create([DataSourceRequest]DataSourceRequest request, ViewModel model)
        {
            var dbModel = base.Create<Model>(model);
            if (dbModel != null) model.Id = dbModel.Id;

            return this.GridOperation(model, request);
        }

        protected override System.IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, System.AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            return base.BeginExecute(requestContext, callback, state);
        }
    }
}