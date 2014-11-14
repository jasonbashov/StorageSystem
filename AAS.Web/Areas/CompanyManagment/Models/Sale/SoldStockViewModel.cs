namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using AAS.Models;
    using AAS.Web.Infrastructure;

    public class SoldStockViewModel : IMapFrom<SoldStock>
    {
        public string Name { get; set; }

        public double SingleUnitPrice { get; set; }

        public int CompanyId { get; set; }

        public decimal SumPrice { get; set; }

        public int QuantitySold { get; set; }
    }
}