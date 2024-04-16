using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.DTOs
{
    public class RecipeDTO
    {
        public int BakeTime { get; set; }
        public List<IngredientDTO> Ingredients { get; set; }
        public List<InstructionDTO> Instructions { get; set; }
    }
}
