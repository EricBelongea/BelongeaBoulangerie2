using BelongeaBoulangerie.DataContext.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recipes = new HashSet<Recipe>();
        }
        public int IngredientId { get; set; }
        public string IngredientString { get; set; }
        public double? Quantity { get; set; }
        public double? Grams { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
        //public ICollection<IngredientRecipe> RecipeLinks { get; set;}
    }
}
