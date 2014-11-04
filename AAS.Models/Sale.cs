using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AAS.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime DateOfSale { get; set; }

        [Required]
        public decimal TotalCost { get; set; }

        [Required]
        public int ClientId { get; set; }

        public virtual Client Client { get; set; }

        [Required]
        public int CompanyId { get; set; }
        
        public virtual Company Company { get; set; }
    }
}
