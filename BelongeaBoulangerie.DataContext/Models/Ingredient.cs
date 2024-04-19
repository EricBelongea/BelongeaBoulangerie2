using BelongeaBoulangerie.DataContext.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Ingredient
    {
        public Ingredient()
        {
            Recipes = new List<Recipe>();
        }
        [JsonIgnore]
        public int IngredientId { get; set; }
        public string IngredientString { get; set; }
        public double? Quantity { get; set; }
        public string? UnitMeasure { get; set; }
        public double? Grams { get; set; }
        [JsonIgnore]
        public ICollection<Recipe> Recipes { get; set; }
    }
}
