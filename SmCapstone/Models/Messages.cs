namespace SmCapstone.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Messages
    {
        [Key]
        public int IdMessage { get; set; }

        [Required]
        public string Message { get; set; }

        [Column(TypeName = "date")]
        public DateTime Date { get; set; }

        public bool IsRead { get; set; }

        public int IdUserSender { get; set; }

        public int IdUserReceiving { get; set; }

        public int IdChat { get; set; }

        public virtual Chats Chats { get; set; }

        public virtual Users Users { get; set; }

        public virtual Users Users1 { get; set; }
    }
}
