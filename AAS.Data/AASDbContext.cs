namespace AAS.Data
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    using AAS.Models;
    using AAS.Data.Migrations;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAASDbContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
        public IDbSet<Stock> Stocks { get; set; }
        
        public IDbSet<Client> Clients { get; set; }
        
        public IDbSet<Company> Companies { get; set; }
        
        public IDbSet<Owner> Owners { get; set; }
        
        public IDbSet<Sale> Sales { get; set; }
    }
}
