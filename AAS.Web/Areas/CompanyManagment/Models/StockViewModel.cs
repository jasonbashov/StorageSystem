namespace AAS.Web.Areas.CompanyManagment.Models
{
    using AAS.Models;
    using AAS.Web.Areas.CompanyManagment.Models.Base;
    using AAS.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class StockViewModel : CompanyManagmentViewModel, IMapFrom<Stock>
    {
        [HiddenInput(DisplayValue = false)]
        public int? Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int CompanyId { get; set; }
    }
}