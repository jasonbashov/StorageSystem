namespace AAS.Web.Areas.Administration.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;

    using AutoMapper.QueryableExtensions;

    using AAS.Data;
    using AAS.Web.Areas.CompanyManagment.Controllers;
    using AAS.Web.Areas.Administration.Models;

    using Model = AAS.Models.ApplicationUser;
    using ViewModel = AAS.Web.Areas.Administration.Models.UsersViewModel;
    using Kendo.Mvc.UI;
    using System.Globalization;
    using AutoMapper;
    using System.Data.Entity;

    public class UserGridController : KendoGridManageController
    {
        public UserGridController(IAASData data)
            : base(data)
        {
        }

        public ActionResult Index()
        {
            return View();
        }

        protected override IEnumerable GetData()
        {
            return this.Data.Users.All().Project().To<UsersViewModel>();
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.GetById(id) as T;
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
                this.Data.Users.Delete(model.Id);
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        protected override System.IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, System.AsyncCallback callback, object state)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            return base.BeginExecute(requestContext, callback, state);
        }
    }
}