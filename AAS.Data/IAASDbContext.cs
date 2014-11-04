namespace AAS.Data
{
    using System.Data.Entity;

    using AAS.Models;

    interface IAASDbContext
    {
        IDbSet<Stock> Stocks { get; set; }
    }
}
