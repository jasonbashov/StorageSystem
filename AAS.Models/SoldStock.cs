namespace AAS.Models
{
    using AAS.Contracts;

    using System.ComponentModel.DataAnnotations;

    public class SoldStock : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
                
        [Required]
        public double SingleUnitPrice { get; set; }
        
        [Required]
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }

        public decimal SumPrice { get; set; }

        public int QuantitySold { get; set; }

    }
}
