namespace AAS.Web.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    public class CompanyInputModel
    {
        [Required]
        [UIHint("TextEditor")]
        [Display(Name = "Name")]
        [AllowHtml]
        public string Name { get; set; }
        
        [Required]
        [UIHint("TextEditor")]
        [Display(Name = "Adress")]
        public string Adress { get; set; }
        
        [Required]
        [StringLength(11, MinimumLength = 11)]
        [UIHint("TextEditor")]
        [Display(Name = "Bulstat")]
        public string Bulstat { get; set; }

        [Required]
        [UIHint("TextEditor")]
        [Display(Name = "Accountable Person")]
        public string AccountablePerson { get; set; }
        
        [UIHint("TextEditor")]
        [Display(Name = "Image Url")]
        public string ImgUrl { get; set; }

    }
}