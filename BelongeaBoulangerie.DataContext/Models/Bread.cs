﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Bread
    {
        public int BreadId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        public string Description { get; set; }
        /* Could Make Ingredients, Instructions, and CookTime attributes of a 'Recipe' Class. 1:1 relationship. Could Also make a Pastry class*/
        public string Ingredients { get; set; }
        public string Instructions { get; set; }
        public Country Country { get; set; }
        public int CountryID { get; set; }
    }
}