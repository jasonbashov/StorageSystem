namespace AAS.Web.Models.Company
{
    using AAS.Web.Infrastructure;
    using AAS.Models;

    public class CompanyDetailsViewModel : IMapFrom<Company>
    {
        public int Id { get; set; }

        public string Name { get; set; }
        
        public int OwnerId { get; set; }

        public virtual Owner Owner { get; set; }

        public string Adress { get; set; }
        
        public string Bulstrad { get; set; }
                
        public string ImgUrl { get; set; }

    }
}