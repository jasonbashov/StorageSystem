namespace AAS.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    using AAS.Models;
    using AAS.Web.Infrastructure;
    using System.Web.Mvc;
    using System;

    public class UsersViewModel : IMapFrom<ApplicationUser>
    {
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        public bool IsDeleted { get; set; }

        public string Email { get; set; }
        
        //public ICollection<string> Roles { get; set; }
    }
}