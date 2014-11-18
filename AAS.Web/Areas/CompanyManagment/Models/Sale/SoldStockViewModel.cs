namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;

    public class SoldStockViewModel : IMapFrom<SoldStock>
    {
        public string Name { get; set; }
        
        [UIHint("PriceDouble")]
        public double SingleUnitPrice { get; set; }

        public int CompanyId { get; set; }
        
        [UIHint("Price")]
        public decimal SumPrice { get; set; }

        public int QuantitySold { get; set; }
    }
}