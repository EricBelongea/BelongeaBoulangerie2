using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.DTOs
{
    public class BreadRecipeDTO
    {
        public int BakeTime { get; set; }
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
    }
}
