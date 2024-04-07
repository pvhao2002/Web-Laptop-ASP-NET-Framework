using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace WebBanLaptop.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
            : base("name=DBContext2")
        {
        }

        public virtual DbSet<cart_items> cart_items { get; set; }
        public virtual DbSet<cart> carts { get; set; }
        public virtual DbSet<category> categories { get; set; }
        public virtual DbSet<order_items> order_items { get; set; }
        public virtual DbSet<order> orders { get; set; }
        public virtual DbSet<product> products { get; set; }
        public virtual DbSet<user> users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<cart_items>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<cart>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<category>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<category>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<order_items>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.total_price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<order>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.price)
                .HasPrecision(10, 2);

            modelBuilder.Entity<product>()
                .Property(e => e.description)
                .IsUnicode(false);

            modelBuilder.Entity<product>()
                .Property(e => e.status)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.role)
                .IsUnicode(false);

            modelBuilder.Entity<user>()
                .Property(e => e.status)
                .IsUnicode(false);
        }
    }
}
