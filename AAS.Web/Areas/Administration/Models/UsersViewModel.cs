namespace AAS.Web.Areas.Administration.Models
{
    using System.Collections.Generic;

    using AAS.Models;
    using AAS.Web.Infrastructure;

    public class UsersViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        public string UserName { get; set; }

        //public ICollection<string> Roles { get; set; }
    }
}