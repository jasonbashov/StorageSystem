﻿namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    using System.Collections;
    using System.Linq;
    using System.Web.Mvc;
    using AAS.Web.Areas.CompanyManagment.Models.Base;
    using Kendo.Mvc.UI;
    using Kendo.Mvc.Extensions;
    using System.Data.Entity;

    using AutoMapper;
    using AutoMapper.QueryableExtensions;

    using AAS.Web.Areas.CompanyManagment.Models;
    using AAS.Contracts;
    using AAS.Data;
    using AAS.Web.Controllers;

    public abstract class KendoGridManageController : BaseController
    {
        public KendoGridManageController(IAASData data)
            : base(data)
        {
        }

        protected abstract IEnumerable GetData();

        protected abstract T GetById<T>(object id) where T : class;

        [HttpPost]
        public ActionResult Read([DataSourceRequest]DataSourceRequest request)
        {
            var data = this.GetData();

            if (data == null)
            {
                return this.Json(data);
            }

            var ads = data.ToDataSourceResult(request);

            return this.Json(ads);
        }

        [NonAction]
        protected virtual T Create<T>(object model) where T : class
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = Mapper.Map<T>(model);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Added);
                return dbModel;
            }

            return null;
        }

        [NonAction]
        protected virtual void Update<TModel, TViewModel>(TViewModel model, object id)
            where TModel : AuditInfo
            where TViewModel : CompanyManagmentViewModel
        {
            if (model != null && ModelState.IsValid)
            {
                var dbModel = this.GetById<TModel>(id);
                Mapper.Map<TViewModel, TModel>(model, dbModel);
                this.ChangeEntityStateAndSave(dbModel, EntityState.Modified);
                model.ModifiedOn = dbModel.ModifiedOn;
            }
        }

        protected JsonResult GridOperation<T>(T model, [DataSourceRequest]DataSourceRequest request)
        {
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }

        private void ChangeEntityStateAndSave(object dbModel, EntityState state)
        {
            var entry = this.Data.Context.Entry(dbModel);
            entry.State = state;
            this.Data.SaveChanges();
        }
    }
}