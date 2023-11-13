using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SmCapstone.Models
{
    public partial class Context : DbContext
    {
        public Context()
            : base("name=Context")
        {
        }

        public virtual DbSet<Bids> Bids { get; set; }
        public virtual DbSet<Categories> Categories { get; set; }
        public virtual DbSet<Chats> Chats { get; set; }
        public virtual DbSet<Messages> Messages { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<Transactions> Transactions { get; set; }
        public virtual DbSet<Users> Users { get; set; }
        public virtual DbSet<Reviews> Reviews { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bids>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Bids>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.Bids)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Categories>()
                .HasMany(e => e.Products)
                .WithRequired(e => e.Categories)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Chats>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Chats)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Products>()
                .Property(e => e.Price)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .Property(e => e.DeliveryPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Products>()
                .HasMany(e => e.Bids)
                .WithRequired(e => e.Products)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Chats)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.IdUserOne)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Chats1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.IdUserTwo)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.IdUserReceiving)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Messages1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.IdUserSender)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reviews)
                .WithRequired(e => e.Users)
                .HasForeignKey(e => e.IdUser);

            modelBuilder.Entity<Users>()
                .HasMany(e => e.Reviews1)
                .WithRequired(e => e.Users1)
                .HasForeignKey(e => e.IdReviewer)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reviews>()
                .Property(e => e.Rating)
                .HasPrecision(18, 1);
        }
    }
}
