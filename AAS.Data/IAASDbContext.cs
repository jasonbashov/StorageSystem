namespace AAS.Data
{
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;

    using AAS.Models;

    public interface IAASDbContext
    {
        IDbSet<Stock> Stocks { get; set; }

        DbContext DbContext { get; }

        int SaveChanges();

        void Dispose();

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        IDbSet<T> Set<T>() where T : class;
    }
}
