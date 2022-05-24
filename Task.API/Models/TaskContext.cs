using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Task.API.Models
{
    public partial class TaskContext : DbContext
    {
        public TaskContext()
            : base("name=TaskContext")
        {
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Transaction> Transactions { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                    .MapToStoredProcedures();
            modelBuilder.Entity<Transaction>()
                 .MapToStoredProcedures();


            modelBuilder.Entity<Transaction>()
                .Property(e => e.Position)
                .IsUnicode(false);

            modelBuilder.Entity<Transaction>()
                .Property(e => e.CompanyName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.EmailId)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Transactions)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);
        }
    }
}
