namespace AAS.Web.Areas.Administration.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    
    using AAS.Models;
    using AAS.Web.Infrastructure;

    public class CompanyViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public string Adress { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Bulstat { get; set; }
        
        public string AccountablePerson { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}