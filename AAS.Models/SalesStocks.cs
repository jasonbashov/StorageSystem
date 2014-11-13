using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAS.Models
{
    public class SalesStocks
    {
        [Key]
        [Column(Order=1)] 
        //[ForeignKey("Sale")]
        public int SaleId { get; set; }
        //public virtual Sale Sale { get; set; }

        [Key]
        [Column(Order=2)]
        //[ForeignKey("Stock")] 
        public int StockId { get; set; }
        //public virtual Stock Stock { get; set; }
    }
}
