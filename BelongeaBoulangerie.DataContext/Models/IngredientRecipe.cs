using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class IngredientRecipe
    {
        public Ingredient Ingredient { get; set; }
        public int IngredientId { get; set; }
        public Recipe Recipe { get; set; }
        public int RecipeID { get; set; } 

    }
}
