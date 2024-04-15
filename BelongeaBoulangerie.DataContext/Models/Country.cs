using System.ComponentModel.DataAnnotations;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Country
    {
        public Country()
        {
            Breads = new HashSet<Bread>();
        }
        public int CountryId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CulinaryHistory { get; set; }
        public string ImageUrl { get; set; }
        public ICollection<Bread> Breads { get; set; }
        public Continent CountryContinent { get; set; }
    }
}
