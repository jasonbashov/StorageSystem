namespace AAS.Models
{
    using AAS.Contracts;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Client : IAuditInfo, IDeletableEntity
    {
        private ICollection<Company> companies;

        public Client()
        {
            this.companies = new HashSet<Company>();
            this.CreatedOn = DateTime.Now;
        }

        public DateTime CreatedOn { get; set; }

        public bool PreserveCreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        [Index]
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Adress { get; set; }

        [Required]
        [StringLength(11, MinimumLength = 11)]
        public string Bulstat { get; set; }

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
