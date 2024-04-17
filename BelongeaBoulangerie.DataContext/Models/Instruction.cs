using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
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
        [JsonIgnore]
        public ICollection<Recipe> Recipes { get; set; }
    }
}
