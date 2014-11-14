namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;

    public class SaleViewModel : IMapFrom<Sale>
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOfSale { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int ClientId { get; set; }
        
        [Required]
        public int CompanyId { get; set; }

        public virtual ICollection<SoldStockViewModel> SoldStocks {get; set; }
        
    }
}