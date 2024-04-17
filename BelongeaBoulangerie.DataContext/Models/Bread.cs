using BelongeaBoulangerie.DataContext.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
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
        public Country Country { get; set; }
        public int CountryID { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
