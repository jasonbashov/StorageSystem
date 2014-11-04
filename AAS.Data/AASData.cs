namespace AAS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    
    using AAS.Data.Repositories;
    using AAS.Models;

    public class AASData : IAASData
    {
        private DbContext context;
        private IDictionary<Type, object> repositories;

        public AASData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetRepository<ApplicationUser>();
            }
        }

        public IRepository<Client> Clients
        {
            get
            {
                return this.GetRepository<Client>();
            }
        }

        public IRepository<Company> Companies
        {
            get
            {
                return this.GetRepository<Company>();
            }
        }

        public IRepository<Owner> Owners
        {
            get
            {
                return this.GetRepository<Owner>();
            }
        }

        public IRepository<Sale> Sales
        {
            get
            {
                return this.GetRepository<Sale>();
            }
        }

        public IRepository<Stock> Stocks
        {
            get
            {
                return this.GetRepository<Stock>();
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}
