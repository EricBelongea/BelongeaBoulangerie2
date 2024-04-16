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
        public string Name { get; set; }
        public string Description { get; set; }
        public RecipeDTO Recipe { get; set; }
        public string CountryName { get; set; }
    }
}
