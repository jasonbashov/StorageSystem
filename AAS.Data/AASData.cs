namespace AAS.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    
    using AAS.Data.Repositories;
    using AAS.Models;
    using AAS.Contracts;

    public class AASData : IAASData
    {
        private IAASDbContext context;
        private IDictionary<Type, object> repositories;

        public AASData(IAASDbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<ApplicationUser> Users
        {
            get
            {
                return this.GetDeletableEntityRepository<ApplicationUser>();
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
                return this.GetDeletableEntityRepository<Sale>();
            }
        }

        public IRepository<Stock> Stocks
        {
            get
            {
                return this.GetDeletableEntityRepository<Stock>();
            }
        }

        public IAASDbContext Context
        {
            get
            {
                return this.context;
            }
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        public void Dispose()
        {
            this.Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                }
            }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(GenericRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IRepository<T>)this.repositories[typeof(T)];
        }

        private IDeletableEntityRepository<T> GetDeletableEntityRepository<T>() where T : class, IDeletableEntity
        {
            if (!this.repositories.ContainsKey(typeof(T)))
            {
                var type = typeof(DeletableEntityRepository<T>);
                this.repositories.Add(typeof(T), Activator.CreateInstance(type, this.context));
            }

            return (IDeletableEntityRepository<T>)this.repositories[typeof(T)];
        }
    }
}
