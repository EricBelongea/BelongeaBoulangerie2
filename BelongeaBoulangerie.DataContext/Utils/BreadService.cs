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

        public async Task<IEnumerable<Bread>> AllBreadWithRecipeInfo()
        {
            return await _context.Breads.Include(b => b.Recipe)
                                           .ThenInclude(r => r.Ingredients)
                                       .Include(b => b.Recipe)
                                           .ThenInclude(r => r.Instructions)
                                       .ToListAsync();
        }

        public async Task<Bread> CreateBread(Bread bread)
        {
            var newBread = new Bread
            {
                Name = bread.Name,
                Description = bread.Description,
                CountryID = bread.CountryID
            };

            var newRecipe = new Recipe
            {
                BakeTime = bread.Recipe.BakeTime,
                Bread = newBread
            };
            
            foreach (var ingredient in bread.Recipe.Ingredients)
            {
                var newIngredient = new Ingredient
                {
                    IngredientString = ingredient.IngredientString,
                    Quantity = ingredient.Quantity,
                    UnitMeasure = ingredient.UnitMeasure,
                    Grams = ingredient.Grams,
                    Recipes = (ICollection<Recipe>)newRecipe
                };
                newRecipe.Ingredients.Add(newIngredient);
            }

            foreach (var instruction in bread.Recipe.Instructions)
            {
                var newInstruction = new Instruction
                {
                    InstructionString = instruction.InstructionString,
                    Recipes = (ICollection<Recipe>)newRecipe
                };
                newRecipe.Instructions.Add(newInstruction);
            }
            _context.Breads.Add(newBread);
            await _context.SaveChangesAsync();

            return bread;
        }
    }
}
