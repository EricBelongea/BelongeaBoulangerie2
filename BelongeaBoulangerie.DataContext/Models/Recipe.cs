using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Recipe
    {
        public Recipe()
        {
            Ingredients = new List<Ingredient>();
            Instructions = new List<Instruction>();
        }
        public int RecipeId { get; set; }
        public int BakeTime { get; set; }
        public Bread Bread { get; set; }
        [ForeignKey("Bread")]
        public int BreadId { get; set; }
        public ICollection<Ingredient> Ingredients { get; set; }
        public ICollection<Instruction> Instructions { get; set; }
    }
}
