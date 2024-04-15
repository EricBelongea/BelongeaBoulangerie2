﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Models
{
    public class Recipe
    {
        public int BakeTime { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Instructions { get; set; }
    }
}
