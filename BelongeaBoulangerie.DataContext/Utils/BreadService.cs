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
    }
}
