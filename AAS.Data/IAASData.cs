namespace AAS.Data
{
    using AAS.Models;

    using AAS.Data.Repositories;

    public interface IAASData
    {
        IRepository<ApplicationUser> Users { get; }

        IRepository<Client> Clients { get; }

        IRepository<Company> Companies { get; }

        IRepository<Owner> Owners { get; }

        IRepository<Sale> Sales { get; }

        IRepository<Stock> Stocks { get; }

        int SaveChanges();
    }
}
