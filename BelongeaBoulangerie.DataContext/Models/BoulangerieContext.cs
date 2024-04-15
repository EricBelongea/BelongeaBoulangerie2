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
        public virtual DbSet<Ingredient> Ingredients { get; set; }
        public virtual DbSet<Instruction> Instructions { get; set; }
        public virtual DbSet<Recipe> Recipes { get; set; }

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

            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(e => e.RecipeId);
            });

            modelBuilder.Entity<Ingredient>(entity =>
            {
                entity.HasKey(e => e.IngredientId);
            });

            modelBuilder.Entity<Instruction>(entity =>
            {
                entity.HasKey(e => e.InstructionId);
            });

            #region BreadData
            //modelBuilder.Entity<Bread>().HasData(
            //    new Bread
            //    {
            //        BreadId = 1,
            //        Name = "Struan Bread",
            //        Description = "A richly textured bread featuring a blend of whole grains, including cornmeal, oats, and wheat bran, combined with the sweetness of honey and the nuttiness of brown rice. This recipe yields two loaves, perfect for sandwiches, and is topped with a choice of poppy or sesame seeds for garnish.",
            //        CountryID = 1,
            //        Ingredients = new List<string> { "638g unbleached bread flour", "42.5g coarse cornmeal (polenta grind)", "28.5g rolled oats", "21g wheat bran or oat bran", "56.5g cooked brown rice", "56.5g brown sugar", "19g salt, or coarse kosher salt", "19g instant yeast", "28.5g honey or agave nectar", "340g lukewarm water (about 35°C)", "113g lukewarm buttermilk, yogurt, or any other milk", "Poppy or sesame seeds for garnish" },
            //        Instructions = new List<string> { "Combine flour, cornmeal, oats, bran, rice, sugar, salt, yeast, honey, water, and milk in a mixing bowl. Mix on the lowest speed for 2 minutes with a paddle attachment or stir by hand for 2 minutes, then let rest for 5 minutes.", "Mix again on the slowest speed for 2 more minutes. The dough should be soft and tacky or slightly sticky. Adjust with flour or water if necessary.", "Transfer the dough to a lightly floured surface and knead lightly for 2 to 3 minutes. It should form a soft, supple ball.", "Fold the dough in half by stretching it out and folding it back onto itself, repeating from each side to create structure. Place in a lightly oiled bowl, cover, and let sit for 10 minutes. Repeat this process 3 more times. After the last repetition, cover the bowl tightly with plastic wrap and refrigerate overnight or up to 5 days.", "Shape the cold dough into two loaves and place in greased loaf pans. Brush the tops with water, sprinkle with seeds, and cover with oiled plastic wrap. Let rise for 1 1/2 to 2 hours, until 1 1/2 times the original size and domed at least 1 inch above the pan.", "Preheat the oven to 350°F (177°C) after about an hour of rising. Bake for 20 minutes, rotate the pan, and bake for another 25-40 minutes, until golden brown and hollow-sounding when tapped. Total baking time is 45-60 minutes.", "Cool, slice, and serve." },
            //        Recipe = new Recipe
            //        {
            //            BakeTime = 60
            //        }
            //    });
            //var breadList = new Bread[]
            //{
            //    new Bread
            //    {
            //        BreadId = 2,
            //        Name = "Highland Oat Bread",
            //        Description = "A Scottish classic, this oat bread combines the nutty flavor of oats with a touch of honey. It's a dense and wholesome bread, perfect for toasting and enjoying with butter or jam.",
            //        CountryID = 1,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 65,
            //            Ingredients = { "400g wholemeal flour", "100g oats", "10g salt", "15g honey", "7g active dry yeast", "300ml warm water" },
            //            Instructions = { "Mix oats, flour, and salt.", "Dissolve yeast in warm water with honey.", "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.", "Shape into a loaf, rise for an additional 30 minutes, and bake at 375°F (190°C) for 25-30 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 3,
            //        Name = "Cranachan Swirl Bread",
            //        Description = "Inspired by the Scottish dessert Cranachan, this swirl bread is filled with raspberries, honey, and toasted oats. A sweet and fruity treat to brighten your day.",
            //        CountryID = 1,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 70,
            //            Ingredients = { "500g strong white flour", "10g salt", "7g active dry yeast", "300ml warm milk", "50g honey", "Filling: Raspberries, honey, toasted oats" },
            //            Instructions = { "Mix flour and salt.", "Dissolve yeast in warm milk with honey.", "Knead the dough, let it rise for 1-2 hours.", "Roll out the dough, spread with honey, raspberries, and oats.", "Roll into a swirl, rise for 30 minutes, and bake at 375°F (190°C) for 25-30 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 4,
            //        Name = "Scottish Bannocks",
            //        Description = "These Scottish Bannocks blend the wholesome goodness of oat and sprouted wheat flours with the tang of sourdough discard, creating a healthy, versatile quick bread. Ready in about 30 minutes, they're perfect for a fiber-rich breakfast or as a base for savory sandwiches.",
            //        CountryID = 1,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 50,
            //            Ingredients = { "130g oat flour", "130g sprouted whole wheat flour", "4g baking powder", "2g baking soda", "3g salt", "185g milk (for version with sourdough starter) or 255g buttermilk (for version without starter)", "140g sourdough starter discard (for sourdough version)", "1-2 Tbsp oil for cooking and shaping", "70g additional flour of choice (for non-sourdough version)" },
            //            Instructions = { "Whisk together oat flour, sprouted wheat flour, baking powder, baking soda, and salt in a bowl.", "For the sourdough version, mix milk with sourdough starter discard. For the non-sourdough version, use buttermilk.", "Combine wet and dry ingredients, kneading briefly to incorporate fully.", "Divide dough into four parts, shaping each into a 1/2 inch thick disc.", "Preheat a lightly oiled frying pan over medium heat. Cook bannocks for about 5 minutes on each side until golden brown, ensuring an internal temperature of at least 195°F.", "Serve warm or cool, as desired, with your choice of sweet or savory fillings." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 5,
            //        Name = "Scottish Bannocks",
            //        Description = "These Scottish Bannocks blend the wholesome goodness of oat and sprouted wheat flours with the tang of sourdough discard, creating a healthy, versatile quick bread. Ready in about 30 minutes, they're perfect for a fiber-rich breakfast or as a base for savory sandwiches.",
            //        CountryID = 1,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 60,
            //            Ingredients = { "130g oat flour", "130g sprouted whole wheat flour", "4g baking powder", "2g baking soda", "3g salt", "185g milk (for version with sourdough starter) or 255g buttermilk (for version without starter)", "140g sourdough starter discard (for sourdough version)", "1-2 Tbsp oil for cooking and shaping", "70g additional flour of choice (for non-sourdough version)" },
            //            Instructions = { "Whisk together oat flour, sprouted wheat flour, baking powder, baking soda, and salt in a bowl.", "For the sourdough version, mix milk with sourdough starter discard. For the non-sourdough version, use buttermilk.", "Combine wet and dry ingredients, kneading briefly to incorporate fully.", "Divide dough into four parts, shaping each into a 1/2 inch thick disc.", "Preheat a lightly oiled frying pan over medium heat. Cook bannocks for about 5 minutes on each side until golden brown, ensuring an internal temperature of at least 195°F.", "Serve warm or cool, as desired, with your choice of sweet or savory fillings." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 6,
            //        Name = "Sakura Blossom Buns",
            //        Description = "Inspired by the beauty of cherry blossoms, these buns are filled with a delicate sakura-flavored cream. A delightful and seasonal Japanese treat.",
            //        CountryID = 2,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 90,
            //            Ingredients = { "500g all-purpose flour", "10g sugar", "7g active dry yeast", "250ml warm milk", "Filling: Sakura-flavored cream (whipped cream with sakura essence)" },
            //            Instructions = { "Mix flour, sugar, and yeast.", "Add warm milk, knead, and let it rise for 1-2 hours.", "Roll out the dough, fill with sakura-flavored cream, shape into buns, rise for 30 minutes, and bake at 375°F (190°C) for 20-25 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 7,
            //        Name = "Matcha Swirl Bread",
            //        Description = "This swirl bread features layers of matcha-flavored dough, creating a beautiful and flavorful green swirl. A perfect blend of traditional and modern Japanese flavors.",
            //        CountryID = 2,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 90,
            //            Ingredients = { "400g bread flour", "100g sugar", "7g active dry yeast", "250ml warm milk", "2 tbsp matcha powder" },
            //            Instructions = { "Mix flour, sugar, and yeast.", "Dissolve matcha powder in warm milk.", "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.", "Roll out the dough, shape into a swirl, rise for 30 minutes, and bake at 375°F (190°C) for 25-30 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 8,
            //        Name = "Red Bean Dorayaki Bread",
            //        Description = "Inspired by the popular Japanese sweet dorayaki, this bread is filled with sweet red bean paste. A delightful combination of soft bread and luscious filling.",
            //        CountryID = 2,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 80,
            //            Ingredients = { "500g all-purpose flour", "10g sugar", "7g active dry yeast", "250ml warm milk", "Filling: Sweet red bean paste" },
            //            Instructions = { "Mix flour, sugar, and yeast.", "Add warm milk, knead, and let it rise for 1-2 hours.", "Shape the dough, fill with sweet red bean paste, rise for 30 minutes, and bake at 375°F (190°C) for 20-25 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 9,
            //        Name = "Yuzu Citrus Roll",
            //        Description = "This Japanese-inspired citrus roll is infused with the bright and fragrant flavor of yuzu. A refreshing and tangy bread to enjoy with tea or as a light dessert.",
            //        CountryID = 2,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 85,
            //            Ingredients = { "400g bread flour", "100g sugar", "7g active dry yeast", "250ml warm milk", "Zest and juice of yuzu" },
            //            Instructions = { "Mix flour, sugar, and yeast.", "Add yuzu zest and juice to warm milk.", "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.", "Shape into a roll, rise for 30 minutes, and bake at 375°F (190°C) for 25-30 minutes." },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 10,
            //        Name = "Pão de Queijo",
            //        Description = "Pão de queijo is a famous Brazilian cheese bread made with cassava flour and cheese, resulting in a chewy and cheesy delight.",
            //        CountryID = 3,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 120,
            //            Ingredients = { "500g tapioca flour", "250ml milk", "125g butter", "10g salt", "2 eggs", "200g grated cheese" },
            //            Instructions = { "Boil milk with butter and salt.", "Pour over tapioca flour and mix.", "Let it cool, then add eggs and cheese.", "Form small balls and bake at 375°F (190°C) for 15-20 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 11,
            //        Name = "Beiju",
            //        Description = "Beiju is a traditional Brazilian flatbread made from tapioca flour. It's thin, crispy, and versatile, often served with sweet or savory toppings.",
            //        CountryID = 3,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 30,
            //            Ingredients = { "Tapioca flour", "Water", "Salt", "Toppings of choice" },
            //            Instructions = { "Mix tapioca flour with water and a pinch of salt.", "Cook the mixture on a hot griddle until it forms a thin, crispy layer.", "Add your favorite toppings and enjoy!" }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 12,
            //        Name = "Bolo de Milho",
            //        Description = "Bolo de milho, or cornbread, is a popular Brazilian treat. It's sweet, moist, and often enjoyed with a cup of coffee or as a dessert.",
            //        CountryID = 3,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 30,
            //            Ingredients = { "250g cornmeal", "250ml milk", "2 eggs", "100g sugar", "50g butter", "1 tsp baking powder" },
            //            Instructions = { "Mix cornmeal, sugar, and baking powder.", "Add eggs, milk, and melted butter.", "Bake in a greased pan at 350°F (180°C) for 30-35 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 13,
            //        Name = "Broa",
            //        Description = "Broa is a traditional Brazilian cornbread made with a mix of cornmeal and wheat flour. It has a dense texture and is commonly served with savory dishes.",
            //        CountryID = 3,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 25,
            //            Ingredients = { "300g cornmeal", "200g wheat flour", "250ml milk", "100g sugar", "50g butter", "1 tsp baking powder" },
            //            Instructions = { "Mix cornmeal, wheat flour, sugar, and baking powder.", "Add melted butter and milk.", "Bake in a greased pan at 350°F (180°C) for 25-30 minutes." }
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 14,
            //        Name = "Baguette",
            //        Description = "A quintessential French bread, the baguette is characterized by its long, thin shape, crispy golden crust, and soft, airy interior. It is a symbol of French baking and is versatile, perfect for sandwiches or as an accompaniment to meals.",
            //        CountryID = 4,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 55,
            //            Ingredients = { "-500g all-purpose flour", "10g salt", "7g active dry yeast", "350ml warm water" },
            //            Instructions = {
            //                "In a bowl, dissolve yeast in warm water and let it sit for 5 minutes until foamy.",
            //                "In a large mixing bowl, combine flour and salt.",
            //                "Make a well in the center and pour in the yeast mixture.",
            //                "Mix until a dough forms, then knead on a floured surface for about 10 minutes until smooth.",
            //                "Place the dough in a lightly oiled bowl, cover with a cloth, and let it rise for 1-2 hours or until doubled in size.",
            //                "Punch down the dough, shape it into a baguette, and place on a baking sheet.",
            //                "Let it rise for another 30 minutes.",
            //                "Preheat the oven to 450°F (230°C).",
            //                "Slash the top of the baguette with a sharp knife and bake for 20-25 minutes until golden brown."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 15,
            //        Name = "Pain de Campagne",
            //        Description = "Translating to 'country bread', pain de campagne is a rustic French loaf made with a mix of flours. It has a chewy crust and a slightly tangy flavor, often enhanced by the addition of a natural sourdough starter.",
            //        CountryID = 4,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 35, // Assuming a default bake time of 35 minutes
            //            Ingredients = new List<string> { "400g bread flour", "100g whole wheat flour", "10g salt", "300ml warm water", "100g active sourdough starter" },
            //            Instructions = new List<string>
            //            {
            //                "In a large bowl, combine bread flour, whole wheat flour, and salt.",
            //                "Mix warm water and sourdough starter, then add to the dry ingredients.",
            //                "Knead the dough for 15-20 minutes until it becomes smooth and elastic.",
            //                "Place the dough in a lightly oiled bowl, cover, and let it rise for 3-4 hours or until doubled in size.",
            //                "Shape the dough into a round loaf, place on a floured surface, and let it rise for an additional 1-2 hours.",
            //                "Preheat the oven to 450°F (230°C).",
            //                "Slash the top of the bread and bake for 30-35 minutes until the crust is golden brown."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 16,
            //        Name = "Pain Poilâne",
            //        Description = "Pain Poilâne is a round, sourdough-style bread with a hearty crust and a distinct wheaty flavor. It's named after the renowned Poilâne bakery in Paris known for its commitment to traditional baking methods.",
            //        CountryID = 4,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 0,
            //            Ingredients = { "500g bread flour", "10g salt", "300ml warm water", "100g active sourdough starter" },
            //            Instructions =
            //            {
            //                "Combine bread flour and salt in a large bowl.",
            //                "Mix warm water and sourdough starter, then add to the dry ingredients.",
            //                "Knead the dough for 15-20 minutes until smooth and elastic.",
            //                "Place the dough in a lightly oiled bowl, cover, and let it rise for 4-6 hours or until doubled."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 17,
            //        Name = "Pain Complet",
            //        Description = "Pain complet, or whole wheat bread, is a wholesome and nutty-flavored French bread made with whole wheat flour. It offers a heartier texture and is a nutritious choice.",
            //        CountryID = 4,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 30,
            //            Ingredients = { "500g whole wheat flour", "10g salt", "7g active dry yeast", "350ml warm water" },
            //            Instructions =
            //            {
            //                "Dissolve yeast in warm water and let it sit for 5 minutes until foamy.",
            //                "In a large bowl, combine whole wheat flour and salt.",
            //                "Make a well and pour in the yeast mixture.",
            //                "Mix until a dough forms, then knead on a floured surface for about 10 minutes until smooth.",
            //                "Place the dough in a lightly oiled bowl, cover, and let it rise for 1-2 hours or until doubled in size.",
            //                "Punch down the dough, shape it into a loaf, and place in a greased pan.",
            //                "Let it rise for another 30 minutes.",
            //                "Preheat the oven to 400°F (200°C).",
            //                "Bake for 25-30 minutes until the bread is golden brown."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 18,
            //        Name = "Naan",
            //        Description = "Naan is a traditional Indian flatbread leavened with yogurt and baked in a tandoor. It's soft, fluffy, and perfect for scooping up delicious curries and dips.",
            //        CountryID = 5,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 150,
            //            Ingredients = { "500g all-purpose flour", "1 tsp baking powder", "1 tsp sugar", "1/2 tsp baking soda", "250ml yogurt", "2 tbsp ghee", "Water (as needed)" },
            //            Instructions =
            //            {
            //                "Mix flour, baking powder, sugar, and baking soda.",
            //                "Add yogurt and ghee, knead into a soft dough, and let it rise for 2 hours.",
            //                "Divide the dough, roll into discs, and bake in a preheated tandoor or oven at 500°F (260°C) for 5-7 minutes."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 19,
            //        Name = "Roti",
            //        Description = "Roti is a staple unleavened Indian flatbread made with whole wheat flour. It's simple, versatile, and commonly enjoyed with various curries and vegetables.",
            //        CountryID = 5,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 100,
            //            Ingredients = { "500g whole wheat flour", "Water", "Salt", "Ghee (optional)" },
            //            Instructions =
            //            {
            //                "Mix whole wheat flour and salt.",
            //                "Add water gradually, knead into a smooth dough.",
            //                "Divide into balls, roll into discs, and cook on a hot griddle until puffed and golden.",
            //                "Brush with ghee if desired."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 20,
            //        Name = "Paratha",
            //        Description = "Paratha is a flaky and layered Indian bread often stuffed with spiced potatoes, paneer, or other fillings. It's pan-fried to perfection and enjoyed with chutneys or yogurt.",
            //        CountryID = 5,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 0, // Paratha is cooked on a griddle, so there's no specific bake time
            //            Ingredients = new List<string> { "500g whole wheat flour", "Water", "Salt", "Filling (Aloo Paratha): Boiled and mashed potatoes, Mixed spices (cumin, coriander, chili), Salt, Ghee (for frying)" },
            //            Instructions = new List<string>
            //            {
            //                "Mix flour and salt.",
            //                "Knead with water to make a soft dough.",
            //                "For filling, mix mashed potatoes with spices.",
            //                "Roll out dough, add filling, fold, and roll again.",
            //                "Cook on a hot griddle with ghee until golden."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 21,
            //        Name = "Bhature",
            //        Description = "Bhature is a deep-fried Indian bread often paired with spicy chickpea curry (chhole). It's fluffy, crispy, and a popular choice for festive occasions.",
            //        CountryID = 5,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 0,
            //            Ingredients = { "500g all-purpose flour", "250ml yogurt", "1 tsp baking powder", "1/2 tsp baking soda", "Salt", "Ghee (for frying)" },
            //            Instructions =
            //            {
            //                "Mix flour, baking powder, baking soda, and salt.",
            //                "Add yogurt, knead into a soft dough, and let it rise for 2 hours.",
            //                "Divide into balls, roll into discs, and deep-fry until puffed and golden."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 22,
            //        Name = "Ciabatta",
            //        Description = "Ciabatta is an Italian bread known for its irregular holes, chewy texture, and crisp crust. It's a versatile bread often used for sandwiches or dipping in olive oil.",
            //        CountryID = 6,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 55,
            //            Ingredients = { "500g bread flour", "10g salt", "7g active dry yeast", "350ml warm water" },
            //            Instructions =
            //            {
            //                "Mix flour and salt.",
            //                "Dissolve yeast in warm water.",
            //                "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.",
            //                "Shape the dough, rise for 30 minutes, and bake at 425°F (220°C) for 20-25 minutes."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 23,
            //        Name = "Focaccia",
            //        Description = "Focaccia is a flat Italian bread topped with olive oil, herbs, and sometimes vegetables. It has a soft and airy texture, making it a delightful accompaniment to meals.",
            //        CountryID = 6,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 25,
            //            Ingredients = { "500g bread flour", "10g salt", "7g active dry yeast", "350ml warm water", "Toppings: Olive oil, rosemary, cherry tomatoes" },
            //            Instructions =
            //            {
            //                "Mix flour and salt.",
            //                "Dissolve yeast in warm water.",
            //                "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.",
            //                "Shape into a pan, press dimples into the dough, drizzle with olive oil, add toppings, and bake at 425°F (220°C) for 20-25 minutes."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        BreadId = 24,
            //        Name = "Grissini",
            //        Description = "Grissini are thin and crunchy Italian breadsticks. They are often served as appetizers or alongside antipasti, providing a satisfying crunch with each bite.",
            //        CountryID = 6,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 135,
            //            Ingredients ={ "500g bread flour", "10g salt", "7g active dry yeast", "250ml warm water", "Olive oil (for brushing)", "Toppings: Sesame seeds, poppy seeds" },
            //            Instructions =
            //            {
            //                "Mix flour and salt.",
            //                "Dissolve yeast in warm water.",
            //                "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.",
            //                "Roll out the dough, cut into thin strips, and bake at 400°F (200°C) for 12-15 minutes."
            //            },
            //        }
            //    },
            //    new Bread
            //    {
            //        Name = "Pane Casareccio",
            //        Description = "Pane Casareccio is a rustic Italian bread with a thick crust and a chewy crumb. It embodies the traditional and simple essence of Italian baking.",
            //        CountryID = 6,
            //        BreadRecipe = new Recipe
            //        {
            //            BakeTime = 30,
            //            Ingredients = { "500g bread flour", "10g salt", "7g active dry yeast", "350ml warm water" },
            //            Instructions =
            //            {
            //                "Mix flour and salt.",
            //                "Dissolve yeast in warm water.",
            //                "Combine wet and dry ingredients, knead, and let it rise for 1-2 hours.",
            //                "Shape the dough, rise for 30 minutes, and bake at 425°F (220°C) for 25-30 minutes."
            //            },
            //        }
            //    }
            //};
            //modelBuilder.Entity<Bread>().HasData(breadList);
            #endregion

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
