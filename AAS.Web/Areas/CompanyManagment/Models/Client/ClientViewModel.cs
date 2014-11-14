namespace AAS.Web.Areas.CompanyManagment.Models.Client
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    
    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System.ComponentModel.DataAnnotations;

    public class ClientViewModel : IMapFrom<Client>
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