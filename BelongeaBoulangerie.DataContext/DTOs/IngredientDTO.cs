﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.DTOs
{
    public class IngredientDTO
    {
        public string IngredientString {  get; set; }
        public double Quantity { get; set; }
        public double Grams { get; set; }
    }
}
