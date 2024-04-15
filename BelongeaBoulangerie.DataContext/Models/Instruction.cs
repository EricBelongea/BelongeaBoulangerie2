using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Instruction
    {
        public Instruction()
        {
            Recipes = new HashSet<Recipe>();
        }
        public int InstructionId { get; set; }
        public string InstructionString { get; set; }
        public ICollection<Recipe> Recipes { get; set; }
    }
}
