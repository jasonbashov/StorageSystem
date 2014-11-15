namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AAS.Models;
    using AAS.Web.Infrastructure;

    public class NewSaleInputModel : IMapFrom<Sale>
    {
        [Required]
        public DateTime DateOfSale { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int ClientId { get; set; }

        public string ClientBulstrad { get; set; }

        public string ClientName { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }

        public virtual ICollection<SoldStockViewModel> SoldStocks { get; set; }
    }
}