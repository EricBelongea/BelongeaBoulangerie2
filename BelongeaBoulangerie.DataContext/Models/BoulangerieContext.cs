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
                entity.Property(e => e.CountryContinent)
                    .HasConversion(c => c.ToString(),
                        c => !string.IsNullOrWhiteSpace(c) ?
                        (Continent)Enum.Parse(typeof(Continent), c) : Continent.UnAssigned);
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

        #region BreadSeeding
        //public void PopulateBreadInformation()
        //{
        //    var bread1 = new Bread
        //    {
        //        Name = "Struan Bread",
        //        Description = "A richly textured bread featuring a blend of whole grains, including cornmeal, oats, and wheat bran, combined with the sweetness of honey and the nuttiness of brown rice. This recipe yields two loaves, perfect for sandwiches, and is topped with a choice of poppy or sesame seeds for garnish.",
        //        CountryID = 1,
        //        BreadRecipe = new Recipe
        //        {
        //            BakeTime = 60,
        //            Ingredients = { "638g unbleached bread flour", "42.5g coarse cornmeal (polenta grind)", "28.5g rolled oats", "21g wheat bran or oat bran", "56.5g cooked brown rice", "56.5g brown sugar", "19g salt, or coarse kosher salt", "19g instant yeast", "28.5g honey or agave nectar", "340g lukewarm water (about 35°C)", "113g lukewarm buttermilk, yogurt, or any other milk", "Poppy or sesame seeds for garnish" },
        //            Instructions = { "Combine flour, cornmeal, oats, bran, rice, sugar, salt, yeast, honey, water, and milk in a mixing bowl. Mix on the lowest speed for 2 minutes with a paddle attachment or stir by hand for 2 minutes, then let rest for 5 minutes.", "Mix again on the slowest speed for 2 more minutes. The dough should be soft and tacky or slightly sticky. Adjust with flour or water if necessary.", "Transfer the dough to a lightly floured surface and knead lightly for 2 to 3 minutes. It should form a soft, supple ball.", "Fold the dough in half by stretching it out and folding it back onto itself, repeating from each side to create structure. Place in a lightly oiled bowl, cover, and let sit for 10 minutes. Repeat this process 3 more times. After the last repetition, cover the bowl tightly with plastic wrap and refrigerate overnight or up to 5 days.", "Shape the cold dough into two loaves and place in greased loaf pans. Brush the tops with water, sprinkle with seeds, and cover with oiled plastic wrap. Let rise for 1 1/2 to 2 hours, until 1 1/2 times the original size and domed at least 1 inch above the pan.", "Preheat the oven to 350°F (177°C) after about an hour of rising. Bake for 20 minutes, rotate the pan, and bake for another 25-40 minutes, until golden brown and hollow-sounding when tapped. Total baking time is 45-60 minutes.", "Cool, slice, and serve." }
        //        }
        //    };
        //}
        #endregion
    }
}
