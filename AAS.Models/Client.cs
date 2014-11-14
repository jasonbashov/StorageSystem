namespace AAS.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Client
    {
        private ICollection<Company> companies;

        public Client()
        {
            this.companies = new HashSet<Company>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Adress { get; set; }

        [Required]
        public string Bulstrad { get; set; }

        [Required]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Company> Companies
        {
            get
            {
                return this.companies;
            }

            set
            {
                this.companies = value;
            }
        }


    }
}
