using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Models
{
    public class Stock
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public int Quantity { get; set; }

        [Required]
        public double Price { get; set; }

        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
