using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public partial class BoulangerieContext : DbContext
    {
        public BoulangerieContext()
        {
        }

        public BoulangerieContext(DbContextOptions<BoulangerieContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<Bread> Breads { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.LogTo(message => Debug.WriteLine(message))
                    .EnableSensitiveDataLogging();
                optionsBuilder.UseSqlServer("Server=.;Database=BelongeaBoulangerie2;Trusted_Connection=True;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Country>(entity =>
            {
                entity.HasKey(e => e.CountryId);
                //entity.HasKey(e => e.Name); // This is a composite key since country names are unique.
            });

            modelBuilder.Entity<Bread>(entity =>
            {
                entity.HasKey(e => e.BreadId);
                entity.OwnsOne(e => e.BreadRecipe)
                    .ToJson("BreadRecipe")
                    .Property(e => e.Ingredients)
                    .IsRequired();
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                //entity.HasKey(e => new { e.FirstName, e.LastName}); // This is how we would make a Composite Key. However, first and last name combinations are not unique. Could use email!
                entity.HasAlternateKey(e => e.Email); // emails are unique. 
                entity.HasIndex(e => e.UserName).IsUnique(); // I want all usernames to be unique.
                entity.Property(e => e.UserName).HasField("_validUsername");
                //entity.Property<byte[]>("Checksum")
                //        .HasComputedColumnSql("CONVERT(VARBINARY(1024),CHECKSUM([FirstName],[LastName],[UserName]))"); // This will map a shadow property to the db.
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
