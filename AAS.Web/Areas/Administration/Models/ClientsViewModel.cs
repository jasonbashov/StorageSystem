namespace AAS.Web.Areas.Administration.Models
{
    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    public class ClientsViewModel : IMapFrom<Client>
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public string Name { get; set; }

        public string Adress { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Bulstat { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }
    }
}