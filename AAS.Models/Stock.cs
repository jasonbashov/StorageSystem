namespace AAS.Models
{
    using System.ComponentModel.DataAnnotations;

    using AAS.Contracts;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Stock : DeletableEntity
    {
        private ICollection<Sale> sale;

        public Stock()
        {
            this.sale = new HashSet<Sale>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        
        [Required]
        public int Quantity { get; set; }
        
        [Required]
        public double Price { get; set; }
        
        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
        
        public virtual ICollection<Sale> Sales
        {
            get
            {
                return this.sale;
            }

            set
            {
                this.sale = value;
            }
        }
    }
}
