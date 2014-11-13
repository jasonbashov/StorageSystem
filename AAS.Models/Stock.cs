namespace AAS.Models
{
    using System.ComponentModel.DataAnnotations;

    using AAS.Contracts;

    public class Stock : DeletableEntity
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public int Quantity { get; set; }

        public double Price { get; set; }

        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
