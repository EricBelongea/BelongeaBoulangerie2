using BelongeaBoulangerie.DataContext.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BelongeaBoulangerie.DataContext.Utils
{
    public class CountryService
    {
        private readonly BoulangerieContext _context;

        public CountryService(BoulangerieContext context)
        {
            _context = context;
        }

        public async Task<Country> CountryByName(string name)
        {
            var country = await _context.Countries.FirstAsync(c => c.Name == name);
            if (country == null)
            {
                throw new Exception("Country not found");
            }
            return country;
        }
    }
}
