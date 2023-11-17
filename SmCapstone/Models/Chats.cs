namespace SmCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Chats
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Chats()
        {
            Messages = new HashSet<Messages>();
        }

        [Key]
        public int IdChat { get; set; }

        public int IdUserOne { get; set; }

        public int IdUserTwo { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Messages> Messages { get; set; }
    }
}
