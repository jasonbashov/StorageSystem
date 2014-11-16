namespace AAS.Web.Areas.CompanyManagment.Models.Sale
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using AAS.Web.Areas.CompanyManagment.Models.Client;

    public class SaleDetailsViewModel : SaleViewModel
    {
        public ClientViewModel Client { get; set; }
    }
}