namespace AAS.Web.Areas.CompanyManagment.Models.Client
{
    using System.ComponentModel.DataAnnotations;

    using AAS.Models;
    using AAS.Web.Infrastructure;
    
    public class ClientInputModel : IMapFrom<Client>
    {
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Adress { get; set; }

        [Required]
        public string Bulstrad { get; set; }

        public string UserId { get; set; }
    }
}