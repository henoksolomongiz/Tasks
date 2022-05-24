namespace Task.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
          public User()
        {
            Transactions = new HashSet<Transaction>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(50)]
        public string FirstName { get; set; }

        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(50)]
        public string EmailId { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        public DateTime? DateofBirth { get; set; }

         public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
