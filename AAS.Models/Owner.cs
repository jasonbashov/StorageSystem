﻿namespace AAS.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Owner
    {
        private ICollection<Company> companies;

        public Owner()
        {
            this.companies = new HashSet<Company>();
        }

        [Key]
        public int Id { get; set; }

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
