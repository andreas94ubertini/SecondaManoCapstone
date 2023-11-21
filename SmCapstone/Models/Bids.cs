namespace SmCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bids
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Bids()
        {
            Transactions = new HashSet<Transactions>();
        }

        [Key]
        public int IdBid { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Prezzo")]
        public decimal Price { get; set; }

        [StringLength(200)]
        [Display(Name = "Messaggio")]
        public string Message { get; set; }
        [Display(Name = "Data offerta")]
        public DateTime BidDate { get; set; }
       
        public bool? IsAccepted { get; set; }

        public int IdUser { get; set; }

        public int IdProduct { get; set; }

        public virtual Products Products { get; set; }

        public virtual Users Users { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Transactions> Transactions { get; set; }
    }
}
