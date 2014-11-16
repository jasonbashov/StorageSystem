namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations.Schema;

    public class NewSaleInputModel : IMapFrom<Sale>
    {
        [Required]
        [Display(Name = "Date")]
        public DateTime DateOfSale { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int ClientId { get; set; }
        
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string ClientBulstat { get; set; }

        public string ClientName { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public string CompanyName { get; set; }
        
        public virtual ICollection<StockViewModel> SoldStocks { get; set; }
    }
}