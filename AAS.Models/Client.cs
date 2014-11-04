using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AAS.Models
{
    public class Client
    {
        private ICollection<Sale> sales;

        public Client()
        {
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Adress { get; set; }
        
        [Required]
        public string Bulstrad { get; set; }
        
        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Sale> Sales
        {
            get
            {
                return this.sales;
            }

            set
            {
                this.sales = value;
            }
        }


    }
}
