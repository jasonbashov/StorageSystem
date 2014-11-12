using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using AAS.Web.Areas.CompanyManagment.Models;
using AAS.Web.Controllers;
using AAS.Models;

namespace AAS.Web.Areas.CompanyManagment.Controllers
{
    public partial class GridController : BaseController
    {
        public ActionResult Editing_Inline()
        {
            return View();
        }

        //public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        //{
        //    return Json(productService.Read().ToDataSourceResult(request));
        //}

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, StockViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                var companyId = int.Parse(this.Url.RequestContext.RouteData.Values["id"].ToString());

                this.Data.Stocks.Add(new Stock()
                {
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    CompanyId = companyId
                });
                this.Data.SaveChanges();
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, StockViewModel product)
        {
            if (product != null && ModelState.IsValid)
            {
                //productService.Update(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, StockViewModel product)
        {
            if (product != null)
            {
                //productService.Destroy(product);
            }

            return Json(new[] { product }.ToDataSourceResult(request, ModelState));
        }
    }
}