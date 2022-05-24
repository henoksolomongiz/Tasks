namespace Task.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Transaction")]
    public partial class Transaction
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
   
        public DateTime? FromDate { get; set; }
        
        public DateTime? ToDate { get; set; }

        [StringLength(50)]
        public string Position { get; set; }

        [StringLength(50)]
        public string CompanyName { get; set; }

        public int UserId { get; set; }

        public virtual User User { get; set; }
    }
}
