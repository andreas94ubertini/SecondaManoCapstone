namespace SmCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Products
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Products()
        {
            Bids = new HashSet<Bids>();
        }

        [Key]
        public int IdProduct { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(100)]
        public string Brand { get; set; }

        [Required]
        [Display(Name = "Descrizione")]
        public string Description { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Prezzo")]
        public decimal Price { get; set; }

        [Required]
        public string Img1 { get; set; }

        public string img2 { get; set; }

        public string img3 { get; set; }

        public bool IsSold { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Luogo in cui si trova")]
        public string Location { get; set; }

        [Column(TypeName = "money")]
        [Display(Name = "Spese di spedizione")]
        public decimal DeliveryPrice { get; set; }

        public int IdUser { get; set; }

        public int IdCategory { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Bids> Bids { get; set; }

        public virtual Categories Categories { get; set; }

        public virtual Users Users { get; set; }
    }
}
