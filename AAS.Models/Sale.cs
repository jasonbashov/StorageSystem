﻿namespace AAS.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AAS.Contracts;

    public class Sale : DeletableEntity
    {
        private ICollection<SoldStock> soldStocks;

        public Sale()
        {
            this.soldStocks = new HashSet<SoldStock>();
        }

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
        
        public virtual ICollection<SoldStock> SoldStocks
        {
            get
            {
                return this.soldStocks;
            }

            set
            {
                this.soldStocks = value;
            }
        }
    }
}
