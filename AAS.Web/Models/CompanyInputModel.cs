using System.ComponentModel.DataAnnotations;
namespace AAS.Web.Models
{
    public class CompanyInputModel
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Adress { get; set; }
        
        [Required]
        public string Bulstrad { get; set; }

    }
}