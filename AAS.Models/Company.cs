using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace AAS.Models
{
    public class Company
    {
        private ICollection<Client> clients;
        private ICollection<Stock> stocks;
        private ICollection<Sale> sales;

        public Company()
        {
            this.clients = new HashSet<Client>();
            this.stocks = new HashSet<Stock>();
            this.sales = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public int OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string Adress { get; set; }
        
        [Required]
        public string Bulstrad { get; set; }
                
        public string ImgUrl { get; set; }

        public virtual ICollection<Client> Clients
        {
            get
            {
                return this.clients;
            }

            set
            {
                this.clients = value;
            }
        }

        public virtual ICollection<Stock> Stocks
        {
            get
            {
                return this.stocks;
            }

            set
            {
                this.stocks = value;
            }
        }

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
