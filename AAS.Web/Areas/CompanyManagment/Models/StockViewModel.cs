namespace AAS.Web.Areas.CompanyManagment.Models
{
    using AAS.Models;
    using AAS.Web.Infrastructure;

    public class StockViewModel : IMapFrom<Stock>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }
    }
}