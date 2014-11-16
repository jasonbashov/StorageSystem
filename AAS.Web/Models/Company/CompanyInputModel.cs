namespace AAS.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    public class CompanyInputModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Adress { get; set; }
        
        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Bulstat { get; set; }

        [Required]
        public string AccountablePerson { get; set; }

        public string ImgUrl { get; set; }

    }
}