namespace AAS.Web.Controllers
{
    using System.Configuration;
    using System.Threading;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using AAS.Models;
    using AAS.Data;

    public abstract class BaseController : Controller
    {

        protected ApplicationUser CurrentUser
        {
            get
            {
                return this.Data.Users.Find(this.GetUserId());
            }
        }

        protected IAASData Data;

        public BaseController()
            : this(new AASData(new ApplicationDbContext()))
        {
        }

        public BaseController(IAASData data)
        {
            this.Data = data;
        }

        private string GetUserId()
        {
            return Thread.CurrentPrincipal.Identity.GetUserId();
        }
    }
}