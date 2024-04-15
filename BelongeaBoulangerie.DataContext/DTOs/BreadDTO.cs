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
        public BreadRecipeDTO Recipe { get; set; }
        public string CountryName { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
    }
}
