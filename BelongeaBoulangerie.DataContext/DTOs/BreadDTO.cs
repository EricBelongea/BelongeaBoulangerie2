using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.DTOs
{
    public class BreadDTO
    {
        public int BreadId {  get; set; }
        public string BreadName { get; set; }
        public string Description { get; set; }
        public string CountryName { get; set; }
        public int? BakeTime { get; set; }
        public List<IngredientDTO> Ingredients { get; set;}
        public List<InstructionDTO> Instructions { get; set;}

    }
}
