namespace SmCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Transactions
    {
        [Key]
        public int IdTransaction { get; set; }

        public bool IsPaid { get; set; }

        public bool? IsShipping { get; set; }

        [StringLength(50)]
        public string Province { get; set; }

        [StringLength(50)]
        public string Region { get; set; }

        [StringLength(50)]
        public string City { get; set; }

        [StringLength(50)]
        public string Address { get; set; }

        [StringLength(20)]
        public string ZipCode { get; set; }

        public int IdBid { get; set; }

        public virtual Bids Bids { get; set; }
    }
}
