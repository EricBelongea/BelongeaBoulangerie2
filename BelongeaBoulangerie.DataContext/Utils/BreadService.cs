using BelongeaBoulangerie.DataContext.DTOs;
using BelongeaBoulangerie.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Utils
{
    public class BreadService
    {
        private readonly BoulangerieContext _context;
        public BreadService(BoulangerieContext context)
        {
            _context = context;
        }
        public async Task<int> CreateBreadFromDTO(BreadDTO breadDto)
        {
            var bread = new Bread
            {
                Name = breadDto.BreadName,
                Description = breadDto.Description
            };

            var recipe = new Recipe
            {
                BakeTime = breadDto.BakeTime,
                Bread = bread
            };

            foreach (var ingredientDTO in breadDto.Ingredients)
            {
                var ingredient = new Ingredient
                {
                    IngredientString = ingredientDTO.IngredientString,
                    Quantity = ingredientDTO.Quantity,
                    UnitMeasure = ingredientDTO.UnitMeasure,
                    Grams = ingredientDTO.Grams
                };
                recipe.Ingredients.Add(ingredient);
            }

            foreach (var instructionDTO in breadDto.Instructions)
            {
                var instruction = new Instruction
                {
                    InstructionString = instructionDTO.InstructionString
                };
                recipe.Instructions.Add(instruction);
            }

            //var country = await _context.Countries.FirstOrDefaultAsync(c => c.Name == breadDto.CountryName);

            //if (country == null)
            //{
            //    return BadRequest("country not found");
            //}

            _context.Recipes.Add(recipe);
            _context.Breads.Add(bread);
            await _context.SaveChangesAsync();

            return bread.BreadId;
        }
    }
}
