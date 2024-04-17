using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Bread
    {
        public Bread()
        {
            Users = new HashSet<User>();
        }
        public int BreadId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        public Recipe Recipe { get; set; }
        [JsonIgnore]
        public Country Country { get; set; }
        public int CountryID { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
